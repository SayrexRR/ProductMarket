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
    public partial class EditProduct : Window
    {
        private Product product;
        private List<string> categories;
        private ProductService marketService;

        public EditProduct(Product editProduct, List<string> categoryNames)
        {
            InitializeComponent();
            categories = categoryNames;
            product = editProduct;
            marketService = new ProductService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categoryComboBox.ItemsSource = categories;

            nameTextBox.Text = product.Name;
            categoryComboBox.SelectedItem = product.CategoryName;
            priceTextBox.Text = product.Price.ToString();
            stockTextBox.Text = product.InStock.ToString();
        }

        private void saveBotton_Click(object sender, RoutedEventArgs e)
        {
            product.Name = nameTextBox.Text;
            product.CategoryName = categoryComboBox.SelectedItem as string;
            if (decimal.TryParse(priceTextBox.Text, out decimal price))
            {
                product.Price = price;
            }
            else
                product.Price = 0;
            if (int.TryParse(stockTextBox.Text, out int stock))
            {
                product.InStock = stock;
            }
            else
                product.InStock = 0;

            Close();
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saveBotton.IsEnabled = !string.IsNullOrWhiteSpace(nameTextBox.Text);
        }
    }
}
