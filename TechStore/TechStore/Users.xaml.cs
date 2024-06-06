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

namespace TechStore
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        users selectedUsers;
        public Users()
        {
            InitializeComponent();
            DbContextTech.entity = new technicalstoreEntities();
            gridData.ItemsSource = DbContextTech.entity.users.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Catalog add = new Catalog();
            add.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

            if (selectedUsers != null)
            {
                

                DbContextTech.entity.SaveChanges();
            }
        }
    }
}
