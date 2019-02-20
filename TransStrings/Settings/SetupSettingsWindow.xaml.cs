using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RodSoft.TranslationJosmStrings.Settings
{
    /// <summary>
    /// Логика взаимодействия для SetupSettingsWindow.xaml
    /// </summary>
    public partial class SetupSettingsWindow : Window
    {
        public SetupSettingsWindow()
        {
            InitializeComponent();
        }

        void Execute(TranslationJosmStringsSettings settings)
        {
            TextFilesFolderTextBox.Text = settings.TextFilesFolder;
            LangFilesFolderTextBox.Text = settings.LangFilesFolder;
            if(ShowDialog().Value)
            {
                settings.TextFilesFolder = TextFilesFolderTextBox.Text;
                settings.LangFilesFolder = LangFilesFolderTextBox.Text;
                SettingsRegistryDriver.SaveSettings(settings);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
