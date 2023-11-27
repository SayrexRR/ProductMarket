using ProductMarket.BussinessLayer.Models;
using ProductMarket.BussinessLayer.Services;
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

namespace ProductMarket
{
    public partial class AddProduct : Window
    {
        
        private List<string> categories;
        private ProductService productService;

        public Product Product { get; set; }

        public AddProduct(List<string> categoryNames)
        {
            InitializeComponent();
            Product = new Product();
            categories = categoryNames;
            productService = new ProductService();
            addBotton.IsEnabled = false;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categoryComboBox.ItemsSource = categories;
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addBotton.IsEnabled = !string.IsNullOrWhiteSpace(nameTextBox.Text);
        }

        private void addBotton_Click(object sender, RoutedEventArgs e)
        {
            Product.Name = nameTextBox.Text;
            Product.CategoryName = categoryComboBox.SelectedItem as string;
            if (decimal.TryParse(priceTextBox.Text, out decimal price))
            {
                Product.Price = price;
            }
            else
                Product.Price = 0;
            if (int.TryParse(stockTextBox.Text, out int stock))
            {
                Product.InStock = stock;
            }
            else
                Product.InStock = 0;

            Close();
        }
    }
}
