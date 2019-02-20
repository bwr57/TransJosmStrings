using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RodSoft.TranslationJosmStrings.Settings
{
    class SettingsRegistryDriver
    {
        private const string LANG_FILES_FOLDER_KEY = "LangFilesFolder";
        private const string TEXT_FILES_FOLDER_KEY = "TextFilesFolder";
        private const string CURRENT_LANGUAGE_CODE_KEY = "CurrentLanguageCode";

        public TranslationJosmStringsSettings Load(TranslationJosmStringsSettings settings)
        {
            if (settings == null)
                settings = new TranslationJosmStringsSettings();
                RegistryKey registryFolder = Registry.CurrentUser;
            try
            {

                registryFolder = registryFolder.OpenSubKey("Software\\Rodsoft\\TranslationJosmStrings");
                if (registryFolder == null)
                    return settings;
                string langFilesFolder = (string)registryFolder.GetValue(LANG_FILES_FOLDER_KEY, settings.LangFilesFolder);
                if (!String.IsNullOrEmpty(langFilesFolder))
                    settings.LangFilesFolder = langFilesFolder;
                string textFilesFolder = (string)registryFolder.GetValue(TEXT_FILES_FOLDER_KEY, settings.LangFilesFolder);
                if (!String.IsNullOrEmpty(textFilesFolder))
                    settings.TextFilesFolder = textFilesFolder;
                string currentLanguageCode = (string)registryFolder.GetValue(CURRENT_LANGUAGE_CODE_KEY, settings.CurrentLanguageCode);
                if (!String.IsNullOrEmpty(currentLanguageCode))
                    settings.CurrentLanguageCode = currentLanguageCode;
            }
            finally
            {
                if (registryFolder != null)
                {
                    registryFolder.Close();
                    registryFolder.Dispose();
                    registryFolder = null;
                }
            }
            return settings;
        }

        public virtual void Save(TranslationJosmStringsSettings settings)
        {
            if (settings == null)
                settings = new TranslationJosmStringsSettings();
            RegistryKey registryFolder = Registry.CurrentUser;
            try
            {
                registryFolder = registryFolder.CreateSubKey("Software\\Rodsoft\\TranslationJosmStrings", RegistryKeyPermissionCheck.ReadWriteSubTree);
                bool isNotSuccess = true;
                //try
                //{
                //    registryFolder = registryFolder.CreateSubKey("Rodsoft");
                //    registryFolder = registryFolder.CreateSubKey("TranslationJosmStrings");
                //    isNotSuccess = false;
                //}
                //catch
                //{
                //}
                //if (isNotSuccess)
                //{
                //    registryFolder = registryFolder.OpenSubKey("Rodsoft\\TranslationJosmStrings");
                //}
                if (registryFolder == null)
                    return;
                registryFolder.SetValue(LANG_FILES_FOLDER_KEY, settings.LangFilesFolder);
                registryFolder.SetValue(TEXT_FILES_FOLDER_KEY, settings.TextFilesFolder);
                registryFolder.SetValue(CURRENT_LANGUAGE_CODE_KEY, settings.CurrentLanguageCode);
            }
            catch
            { }
            finally
            {
                registryFolder.Close();
                registryFolder.Dispose();
                registryFolder = null;
            }
            
        }

        public static TranslationJosmStringsSettings LoadSettings(TranslationJosmStringsSettings settings)
        {
            SettingsRegistryDriver driver = new SettingsRegistryDriver();
            return driver.Load(settings);
        }

        public static void SaveSettings(TranslationJosmStringsSettings settings)
        {
            SettingsRegistryDriver driver = new SettingsRegistryDriver();
            driver.Save(settings);
        }
    }
}
