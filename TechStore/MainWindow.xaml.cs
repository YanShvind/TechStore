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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TechStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DbContextTech.entity = new technicalstoreEntities();
            ListView1.ItemsSource = DbContextTech.entity.goods.ToList();
            SortBy.ItemsSource = new string[] { "Название", "цена" };
            var enumDirection = new string[] { "по возрастанию", "по убыванию" };

            
            //asdasdasdasdasdasdasdasdasdasdasdasdasd


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
    }
}