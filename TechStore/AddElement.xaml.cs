using Microsoft.Win32;
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
using System.IO;

namespace TechStore
{
    /// <summary>
    /// Логика взаимодействия для AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        string paths;
        string saymyname;
        public AddElement()
        {
            InitializeComponent();
            DbContextTech.entity = new technicalstoreEntities();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var goods = new goods()
            {
                name = nameBox.Text.Trim(),
                price = Convert.ToInt32(priceBox.Text.Trim()),
                description = descBox.Text.Trim(),
                image = DbContextTech.ImageToAdd,

            };

            DbContextTech.entity.goods.Add(goods);
            DbContextTech.entity.SaveChanges();
            File.Copy(paths, System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..", ".."), $"Images2\\{saymyname}")));
            MessageBox.Show("Успешно добавлено");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            catalog.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Выберите картинку";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    paths = op.FileName;
                    saymyname = op.SafeFileName;
                    imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                    DbContextTech.ImageToAdd = op.SafeFileName;
                }
            }
            catch
            {
                MessageBox.Show("Что то пошло не так");
            }
        }
    }
}
