using System;
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
            SortBy.ItemsSource = new string[] { "Название", "цена" };
            var enumDirection = new string[] { "по возрастанию", "по убыванию" };

            sortProp.ItemsSource = enumDirection;
            sortProp.SelectedValue = enumDirection[0];
            SortBy.SelectedValue = "Название";
            ListView1.Items.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));

            sortProp.SelectionChanged += SelectionChanged;
            SortBy.SelectionChanged += SelectionChanged;
        }
        //
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var selectedProduct = ListView1.SelectedItem as goods;

            var cartItem = new basket()
            {
                idbasket = DatabaseFlower.authUserId,
                idgood = selectedProduct.idgood,
                quantity = 1,
                
            };

  

            DatabaseFlower.entity.basket.Add(cartItem);
            DatabaseFlower.entity.SaveChanges();

            }
//
        

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
