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
using Microsoft.Win32;

namespace TechStore
{
    /// <summary>
    /// Логика взаимодействия для ChangeCell.xaml
    /// </summary>
    public partial class ChangeCell : Window
    {
        string paths;
        string saymyname;
        goods selectedGoods;
        public ChangeCell(goods goods)
        {
            InitializeComponent();

            selectedGoods = goods;
            nameBox.Text = selectedGoods.name;
            priceBox.Text = selectedGoods.price.ToString();
            descBox.Text = selectedGoods.description;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (selectedGoods != null)
            {
                selectedGoods.name = nameBox.Text.Trim();
                selectedGoods.price = Convert.ToInt32(priceBox.Text.Trim());
                selectedGoods.description = descBox.Text.Trim();

                DbContextTech.entity.SaveChanges();

                MessageBox.Show("Успешно отредактировано");
                Catalog add = new Catalog();
                add.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите ячейку для редактирования");
            }
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            catalog.Show();
            this.Close();
        }
    }
}
