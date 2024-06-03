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

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using QRCoder;

namespace TechStore
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        public Basket()
        {
            InitializeComponent();
            DbContextTech.entity = new technicalstoreEntities();
            ListView2.ItemsSource = DbContextTech.entity.basket.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DbContextTech.entity.basket.Any())
            {
                Document doc = new Document();
                string filePath = "C:\\Users\\10210815\\Source\\Repos\\YanShvind\\TechStore\\TechStore\\pdf\\check.pdf";

                try
                {
                    PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();

                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Чек покупки");
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);
                    doc.Add(new iTextSharp.text.Paragraph("\n"));

                    List<basket> basketItems = DbContextTech.entity.basket.ToList();

                    decimal totalAmount = 0; 

                    foreach (var item in basketItems)
                    {
                        goods product = DbContextTech.entity.goods.FirstOrDefault(g => g.idgood == item.idgood);
                        if (product != null)
                        {
                            string itemInfo = $"{product.name} - {item.quantity} шт. x {product.price} руб.";
                            doc.Add(new iTextSharp.text.Paragraph(itemInfo));

                            totalAmount += (decimal)(item.quantity.GetValueOrDefault() * product.price);
                        }
                    }

                    iTextSharp.text.Paragraph totalAmountParagraph = new iTextSharp.text.Paragraph($"Итог: {totalAmount} руб.");
                    totalAmountParagraph.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(totalAmountParagraph);

                    doc.Close();

                    System.Diagnostics.Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при создании PDF: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Корзина пуста. Невозможно создать чек.");
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            catalog.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var item = button.DataContext as basket;

            if (item != null)
            {

                DbContextTech.entity.basket.Remove(item);


                DbContextTech.entity.SaveChanges();


                ListView2.ItemsSource = DbContextTech.entity.basket.ToList();
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var item = button.DataContext as basket;

            if (item != null && item.quantity > 0)
            {
                item.quantity -= 1;

                if (item.quantity == 0)
                {
                    DbContextTech.entity.basket.Remove(item);
                }

                DbContextTech.entity.SaveChanges();

                ListView2.ItemsSource = DbContextTech.entity.basket.ToList();
            }
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var item = button.DataContext as basket;

            if (item != null)
            {
                item.quantity += 1;

                DbContextTech.entity.SaveChanges();

                ListView2.ItemsSource = DbContextTech.entity.basket.ToList();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (DbContextTech.entity.basket.Any())
            {
                StringBuilder basketDataBuilder = new StringBuilder();
                foreach (var item in DbContextTech.entity.basket)
                {
                    goods product = DbContextTech.entity.goods.FirstOrDefault(g => g.idgood == item.idgood);
                    if (product != null)
                    {
                        string itemInfo = $"{product.name} - {item.quantity}шт x {product.price}руб.";
                        basketDataBuilder.AppendLine(itemInfo);
                    }
                }
                string basketData = basketDataBuilder.ToString();

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(basketData, QRCodeGenerator.ECCLevel.Q);

                QRCode qrCode = new QRCode(qrCodeData);
                System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20); // 20 - размер пикселей

                //System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..", ".."), $"Images1"))

                string qrCodeImagePath = "путь_до_изображения_qr_code.png";
                qrCodeImage.Save(qrCodeImagePath);

                System.Diagnostics.Process.Start(qrCodeImagePath);
            }
            else
            {
                MessageBox.Show("Корзина пуста. Невозможно создать QR-код.");
            }
        }

    }
}
