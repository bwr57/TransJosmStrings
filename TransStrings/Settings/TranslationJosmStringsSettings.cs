using System.IO;
using System.Reflection;

namespace RodSoft.TranslationJosmStrings.Settings
{
    class TranslationJosmStringsSettings
    {
        public string TextFilesFolder;
        public string LangFilesFolder;
        public string CurrentLanguageCode;

    
        public TranslationJosmStringsSettings()
        {
            TextFilesFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LangFilesFolder = TextFilesFolder;
            CurrentLanguageCode = "ru";
        }
    }
}
