using RodSoft.TranslationJosmStrings.Drivers;
using RodSoft.TranslationJosmStrings.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RodSoft.TranslationJosmStrings
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly IList<TranslatedString> TranslatedStrings = new List<TranslatedString>();
        private readonly TranslationJosmStringsSettings Settings = new TranslationJosmStringsSettings();
        private bool _IsChanged = false;

        public MainWindow()
        {
            InitializeComponent();
            SettingsRegistryDriver settingsDriver = new SettingsRegistryDriver();
            settingsDriver.Load(Settings);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadTextData();
        }

        private void LoadTextData()
        {
            if(_IsChanged)
            {
                MessageBoxResult mbResult = MessageBox.Show("Data was changed. Save?", "Translation of Josm strings", MessageBoxButton.YesNoCancel);
                if (mbResult == MessageBoxResult.Cancel)
                    return;
                if (mbResult == MessageBoxResult.Yes)
                    if (!SaveTextData())
                        return;
            }
            TranslatedStrings.Clear();
            try
            {
                TextTranslationFileDriver textTranslationFileDriver = new TextTranslationFileDriver();
                textTranslationFileDriver.Load(TranslationFileDriverBase.ENGLISH_LANGUAGE_CODE, TranslatedStrings);
                textTranslationFileDriver.Load("ru", TranslatedStrings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Translation of Josm strings", MessageBoxButton.OK);
            }
            RefreshDataGrid();
        }

        private bool SaveTextData()
        {
            try
            {
                TextTranslationFileDriver textTranslationFileDriver = new TextTranslationFileDriver();
                textTranslationFileDriver.Save(TranslationFileDriverBase.ENGLISH_LANGUAGE_CODE, TranslatedStrings);
                textTranslationFileDriver.Save("ru", TranslatedStrings);
                _IsChanged = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Translation of Josm strings", MessageBoxButton.OK);
                return false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveTextData();
        }

        private void SaveLangData()
        {
            LangTranslationFileDriver langTranslationDriver = new LangTranslationFileDriver();
            langTranslationDriver.Save(TranslationFileDriverBase.ENGLISH_LANGUAGE_CODE, TranslatedStrings);
            langTranslationDriver.Save("ru", TranslatedStrings);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {                                                                                                                                                                                                                                                                                                            
            SaveLangData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TranslatedStrings.RemoveAt(dgMain.SelectedIndex);
            _IsChanged = true;
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dgMain.ItemsSource = null;
            dgMain.ItemsSource = TranslatedStrings;
        }

        private void dgMain_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _IsChanged = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_IsChanged)
            {
                MessageBoxResult mbResult = MessageBox.Show("Data was changed. Save?", "Translation of Josm strings", MessageBoxButton.YesNoCancel);
                if (mbResult == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (mbResult == MessageBoxResult.Yes)
                    if (!SaveTextData())
                    {
                        e.Cancel = true;
                        return;
                    }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SettingsRegistryDriver.SaveSettings(Settings);
        }
    }
}
