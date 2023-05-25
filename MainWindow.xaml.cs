using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ClassLibrary1; 

namespace Meatsnov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Animal animal = new Animal();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                animal.Name = text1.Text;
            }
            catch { MessageBox.Show("Нельзя вводить латиницу и символы"); }

            try
            {
                animal.Height = Convert.ToInt32(text2.Text);
            }
            catch { MessageBox.Show("Введите целочисленное число"); }

            try
            {
                animal.Width = Convert.ToInt32(text3.Text);
            }
            catch { MessageBox.Show("Введите целочисленное число"); }

            Class1.Serialize(animal, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Animal_har.json");
        }

        private void OutSave(object sender, RoutedEventArgs e)
        {
            animal = Class1.Deserialize<Animal>(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Animal_har.json");

            text1.Text = animal.Name;
            text2.Text = animal.Height.ToString();
            text3.Text = animal.Width.ToString();
        }
    }
    public class Animal
    {
        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                Regex pattern = new Regex(@"^[А-ЯЁ][а-яё]*$");
                if (!pattern.IsMatch(value))
                {
                    throw new ArgumentException("Тут только буквы и пробелы");
                }

                name = value; 
            }
        }
        public int Height;
        public int Width;
    }
}
