﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TechStore
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
        {
            InitializeComponent();
            DbContextTech.entity = new technicalstoreEntities();
            ListView1.ItemsSource = DbContextTech.entity.goods.ToList();

            if (DbContextTech.staff == 1)
            {
                AdminButton.Visibility = Visibility.Visible;
                User.Visibility = Visibility.Visible;
            }

            SortBy.ItemsSource = new string[] { "Название", "цена" };
            var enumDirection = new string[] { "по возрастанию", "по убыванию" };

            sortProp.ItemsSource = enumDirection;
            sortProp.SelectedValue = enumDirection[0];
            SortBy.SelectedValue = "Название";
            ListView1.Items.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));

            sortProp.SelectionChanged += SelectionChanged;
            SortBy.SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFilter = sortProp.SelectedItem.ToString();
            string selectedSort = SortBy.SelectedItem.ToString();
            ListSortDirection sortDirection = selectedFilter.Contains("по возрастанию") ? ListSortDirection.Ascending : ListSortDirection.Descending;

            var view = (CollectionView)CollectionViewSource.GetDefaultView(ListView1.ItemsSource);
            view.SortDescriptions.Clear();

            if (selectedSort == "Название")
                selectedSort = "name";
            if (selectedSort == "цена")
                selectedSort = "price";

            view.SortDescriptions.Add(new SortDescription(selectedSort, sortDirection));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket();
            basket.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            // Получаем данные текущего элемента
            var data = button.DataContext as goods;

            var isGoods = DbContextTech.entity.basket.Any(b => b.idgood == data.idgood);
            var needGoods = DbContextTech.entity.basket.FirstOrDefault(b => b.idgood == data.idgood);
            if (isGoods)
            {
                needGoods.quantity += 1;
                DbContextTech.entity.SaveChanges();
            }
            else
            {
                var cartItem = new basket()
                {
                    idbasket = DbContextTech.authUserId,
                    idgood = data.idgood,
                    quantity = 1,
                };
                DbContextTech.entity.basket.Add(cartItem);
                DbContextTech.entity.SaveChanges();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            var filteredList = DbContextTech.entity.goods.Where(g => g.name.ToLower().Contains(searchText) || g.description.ToLower().Contains(searchText)).ToList();

            ListView1.ItemsSource = filteredList;
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            AddElement add = new AddElement();
            add.Show();
            this.Close();
        }

        private void Button_ClickDel(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var item = button.DataContext as goods;

            if (item != null)
            {

                DbContextTech.entity.goods.Remove(item);


                DbContextTech.entity.SaveChanges();


                ListView1.ItemsSource = DbContextTech.entity.goods.ToList();
            }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_ClickChange(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            // Получаем данные текущего элемента
            var data = button.DataContext as goods;

            ChangeCell add = new ChangeCell(data);
            add.Show();
            this.Close();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            Button button1 = grid.Children[grid.Children.Count - 1] as Button;
            Button button2 = grid.Children[grid.Children.Count - 2] as Button;

            if (DbContextTech.staff == 1)
            {
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;

            }
        }

        private void Button_ClickUsers(object sender, RoutedEventArgs e)
        {
            Users add = new Users();
            add.Show();
            this.Close();
        }
    }
}