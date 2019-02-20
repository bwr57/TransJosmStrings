using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RodSoft.TranslationJosmStrings.Drivers
{
    class LangTranslationFileDriver : TranslationFileDriverBase
    {
        private const string LANG_FILE_EXTENSION = ".lang";

        public override void Load(string languageCode, IList<TranslatedString> translatedStrings)
        {
            throw new NotImplementedException();
        }

        public static void SaveFile(string fileName, IEnumerable<byte[]> data)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    foreach (byte[] bytes in data)
                    {
                        fs.WriteByte(Convert.ToByte(bytes.Length / 256));
                        fs.WriteByte(Convert.ToByte(bytes.Length % 256));
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                catch
                {
                    throw new Exception("Error at saving");
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
            string fileName = languageCode + LANG_FILE_EXTENSION;
            IList<byte[]> data = new List<byte[]>(translatedStrings.Count);
            Encoding enc = new UTF8Encoding(true, true);
            if (String.Equals(languageCode, ENGLISH_LANGUAGE_CODE, StringComparison.CurrentCultureIgnoreCase))
                for (int i = 0; i < translatedStrings.Count; i++)
                {
                    if (String.IsNullOrEmpty(translatedStrings[i].EnText))
                        throw new Exception("English string is empty");
                    //                    data.Add(enc.GetBytes(translatedStrings[i].EnText.Replace("\\u0004", "\u0004")));
                    if (translatedStrings[i].EnText.Contains("\\u0004"))
                        data.Add(enc.GetBytes("_:" + translatedStrings[i].EnText.Replace("\\u0004", "\n")));
                    else
                        data.Add(enc.GetBytes(translatedStrings[i].EnText));

                    if (translatedStrings[i].EnText.IndexOf("\\u") > 0)
                        fileName = fileName;
                    //                    data.Add(GetStringBytes(translatedStrings, i));
                }
            else
                for (int i = 0; i < translatedStrings.Count; i++)
                {
                    data.Add(Encoding.UTF8.GetBytes(translatedStrings[i].RuText));
                }
            SaveFile(fileName, data);
        }

        //private static byte[] GetStringBytes(string text)
        //{
        //    IList<byte[]> subStrings = new List<byte[]>();
        //    int startPosition = 0;
        //    int totalBytes = 0;
        //    int indexOfSpecSymbol = text.IndexOf("\\u");
        //    while(indexOfSpecSymbol >= 0)
        //    {
        //        string subString = text.Substring(startPosition, indexOfSpecSymbol);
        //        byte[] subStringBytes = Encoding.UTF8.GetBytes(subString);
        //        subStrings.Add(subStringBytes);
        //        subString = text.Substring(indexOfSpecSymbol + 2, 4);
        //        int b = 0;
        //        int.TryParse(subString, System.Globalization.NumberStyles.HexNumber, null, out b);

        //    }
        //    return Encoding.UTF8.GetBytes(translatedStrings[i].EnText);
        //}
    }
}
