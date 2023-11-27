using Microsoft.EntityFrameworkCore;
using ProductMarket.Entities;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProductMarket.BussinessLayer.Models;
using System.Windows.Controls;
using System.Data;
using ProductMarket.BussinessLayer.Services;

namespace ProductMarket
{
    public partial class MainWindow : Window
    {
        private const string labelContent = "Products:";
        private readonly ProductService productService;

        private CollectionViewSource productViewSource;

        private List<Product> products;
        private List<string> categoryNames;

        public MainWindow()
        {
            InitializeComponent();
            productService = new ProductService();
            productViewSource = (CollectionViewSource)FindResource(nameof(productViewSource));
            tablesLabel.Content = labelContent;
            products = new List<Product>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            products = productService.GetProducts();
            productViewSource.Source = products;

            categoryNames = productService.GetCategoriesName();

            categoryNameComboBox.Items.Add("All");
            foreach (var categoryName in categoryNames)
            {
                categoryNameComboBox.Items.Add(categoryName);
            }
        }

        private void categoryNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory = (sender as ComboBox).SelectedItem as string;

            if (selectedCategory != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(myDataGrid.ItemsSource);
                view.Filter = item =>
                {
                    Product product = item as Product;

                    if (selectedCategory == "All")
                    {
                        return product != null;
                    }

                    return product != null && product.CategoryName == selectedCategory;
                };
            }
        }

        private void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                Product selectedProduct = myDataGrid.SelectedItem as Product;
                
                if (selectedProduct == null)
                    return;
                
                EditProduct editProductView = new EditProduct(selectedProduct, categoryNames);
                editProductView.ShowDialog();
                
                productService.Update(selectedProduct);

                myDataGrid.Items.Refresh();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            AddProduct addProduct = new AddProduct(categoryNames);
            addProduct.ShowDialog();

            productService.Add(addProduct.Product);

            addProduct.Product.Id = productService.GetLastId();
            products.Add(addProduct.Product);

            myDataGrid.Items.Refresh();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = myDataGrid.SelectedItem as Product;

            if (selectedProduct == null)
                return;

            productService.Delete(selectedProduct.Id);
            products.Remove(selectedProduct);

            myDataGrid.Items.Refresh();
        }
    }
}