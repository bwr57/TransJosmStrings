using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RodSoft.TranslationJosmStrings.Drivers
{
    class TextTranslationFileDriver : TranslationFileDriverBase
    {
        private const string TXT_FILE_EXTENSION = ".txt";
        private const string CHANGE_LINE_SYMBOLS = "\r\n";

        public static string[] LoadText(string fileName)
        {
            string result = "";
            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    try
                    {
                        byte[] bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, bytes.Length);
                        result = Encoding.Default.GetString(bytes);
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            result = result.Replace("\r", "");
            return result.Split("\n".ToCharArray());
        }

        public override void Load(string languageCode, IList<TranslatedString> translatedStrings)
        {
            if (String.IsNullOrEmpty(languageCode))
                languageCode = ENGLISH_LANGUAGE_CODE;
            if (translatedStrings == null)
                translatedStrings = new List<TranslatedString>();
            string[] lines = LoadText(languageCode + TXT_FILE_EXTENSION);
            if (String.Equals(languageCode, ENGLISH_LANGUAGE_CODE, StringComparison.InvariantCultureIgnoreCase))
                for (int i = 0; i < lines.Length; i++)
                {
                    translatedStrings.Add(new TranslatedString() { EnText = lines[i] });
                }
            else
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i < translatedStrings.Count)
                        translatedStrings[i].RuText = lines[i];
                    else
                        translatedStrings.Add(new TranslatedString() { RuText = lines[i] });
                }
        }
    

        public static void SaveText(string fileName, string text)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    byte[] bytes = Encoding.Default.GetBytes(text);
                    fs.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    throw new Exception("Error at saving file");
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        public override void Save(string languageCode, IList<TranslatedString> translatedStrings)
        {
            if (String.IsNullOrEmpty(languageCode) || translatedStrings == null)
                throw new Exception("Illegal language code or translated strings list is null");
            StringBuilder sb = new StringBuilder();
            if (String.Equals(languageCode, ENGLISH_LANGUAGE_CODE, StringComparison.InvariantCultureIgnoreCase))
                for (int i = 0; i < translatedStrings.Count; i++)
                {
                    if (String.IsNullOrEmpty(translatedStrings[i].EnText))
                        throw new Exception("English string is empty");
                    if (i > 0) sb.Append(CHANGE_LINE_SYMBOLS);
                    sb.Append(translatedStrings[i].EnText);
                }
            else
                for (int i = 0; i < translatedStrings.Count; i++)
                {
                    if (i > 0) sb.Append(CHANGE_LINE_SYMBOLS);
                    if (!String.IsNullOrEmpty(translatedStrings[i].RuText))
                        sb.Append(translatedStrings[i].RuText);
                }
            SaveText(languageCode + TXT_FILE_EXTENSION, sb.ToString());
        }
    }
}
