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

namespace TransStrings
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string LoadText(string fileName)
        {
            string result = "";
            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    result = Encoding.Default.GetString(bytes);
                    fs.Close();
                }
            }
            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBoxEnText.Text = LoadText("en.txt");
            textBoxRuText.Text = LoadText("ru.txt");
        }

        private void SaveText(string fileName, string text)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                byte[] bytes = Encoding.Default.GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveText("en.txt", textBoxEnText.Text);
            SaveText("ru.txt", textBoxRuText.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (File.Exists("en.lang"))
                File.Delete("en.lang");
            using (FileStream fs = new FileStream("en.lang", FileMode.OpenOrCreate))
            {
                string[] enText = textBoxEnText.Text.Split("\r\n".ToCharArray());
                for (int i = 0; i < enText.Length; i++)
                {
                    if (enText[i] != "")
                    {
                        byte[] bytes = Encoding.Default.GetBytes(enText[i]);
                        fs.WriteByte(Convert.ToByte(bytes.Length / 256));
                        fs.WriteByte(Convert.ToByte(bytes.Length % 256));
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                fs.Close();
            }
            if (File.Exists("ru.lang"))
                File.Delete("ru.lang");
            using (FileStream fs = new FileStream("ru.lang", FileMode.CreateNew))
            {
                string[] ruText = textBoxRuText.Text.Split("\r\n".ToCharArray());
                for (int i = 0; i < ruText.Length; i++)
                {
                    if (ruText[i] != "")
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(ruText[i]);
                        fs.WriteByte(0);
                        fs.WriteByte(Convert.ToByte(bytes.Length));
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                fs.Close();
            }
        }
    }
}
