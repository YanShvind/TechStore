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
            SortBy.ItemsSource = new string[] { "name", "price" };
            sortProp.ItemsSource = Enum.GetNames(typeof(ListSortDirection));
            ListView1.Items.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));

        }
    }
}
