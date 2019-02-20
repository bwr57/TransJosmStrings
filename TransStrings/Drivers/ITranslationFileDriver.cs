using System.Collections.Generic;

namespace RodSoft.TranslationJosmStrings.Drivers
{
    public interface ITranslationFileDriver
    {
        void Load(string languageCode, IList<TranslatedString> translatedStrings);
        void Save(string languageCode, IList<TranslatedString> translatedStrings);
    }

    public abstract class TranslationFileDriverBase : ITranslationFileDriver
    {
        public const string ENGLISH_LANGUAGE_CODE = "en";
        
        public abstract void Load(string languageCode, IList<TranslatedString> translatedStrings);
        public abstract void Save(string languageCode, IList<TranslatedString> translatedStrings);
    }
}
