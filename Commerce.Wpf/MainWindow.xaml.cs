using Core.Parameters;
using Core.CommandsServices;
using Core.Results;
using Repository.Repositories;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Entities;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core.Ports.Driving;
using Core.Ports.Driven;
using Core.DTO;

namespace Commerce.Wpf
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        IUnitOfWork _database;
        ICommandService<ProductParameter, Result> _insertProductService;
        ICommandService<IdProductParameter, Result> _deleteProductService;
        ICommandService<NullParameter, GetAllProductsResult> _getAllProductService;
        ICommandService<ProductParameter, Result> _updateProductService;
        public List<ProductDTO> ProductDTOs { get {return _products.ToList() ;} set {_products= (List<ProductDTO>)value; OnPropertyChanged("ProductDTOs"); } }
        List<ProductDTO> _products;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = this;
            _database = new UnitOfWork(@"Server=(localdb)\mssqllocaldb;Database=Commerce;Trusted_Connection=True");
            _insertProductService = new InsertProductService(_database.Products);
            _getAllProductService = new GetAllProductService(_database.Products);
            _deleteProductService = new DeleteProductService(_database.Products);
            _updateProductService = new UpdateProductService(_database.Products);
            GetAllProducts();
            ClearItem();
        }

        private void RefreshProduct_Click(object sender, RoutedEventArgs e)
        {
            GetAllProducts();
        }

        private void GetAllProducts()
        {
            IEnumerable<ProductDTO> productDTOs = _getAllProductService.Execute(new NullParameter()).Products;
            if (productDTOs != null)
            {
                ProductDTOs = productDTOs.ToList();
            }
        }
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void btnProductAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductParameter productParameter = new()
            {
                ProductId = Guid.NewGuid(),
                Name = txbName.Text,
                UnitPrice = txbUnitPrice.Text,
                Description = txbDescription.Text
            };

            Result result = _insertProductService.Execute(productParameter);

            if (result.Code == 0)
            {
                _database.Save();
            }

            GetAllProducts();
        }

        private void btnProductClr_Click(object sender, RoutedEventArgs e)
        {
            ClearItem();
        }

        private void dgProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = ((DataGrid)sender).SelectedIndex;
            if (index >= 0)
            {
                ProductDTO product = (ProductDTO)((DataGrid)sender).Items[index];
                if (product != null )
                {
                    txbId.Text = product.Id.ToString();
                    txbName.Text = product.Name.ToString();
                    txbUnitPrice.Text = product.UnitPrice.ToString();
                    txbDescription.Text = product.Description.ToString();
                }
                lbId.Visibility = txbId.Visibility = Visibility.Visible;
                btnProductAdd.IsEnabled = false;
                btnProductDel.IsEnabled = btnProductUpd.IsEnabled = true;
            }
        }

        private void btnProductDel_Click(object sender, RoutedEventArgs e)
        {
            IdProductParameter idProductParameter = new() { ProductId = new(txbId.Text) };

            Result result = _deleteProductService.Execute(idProductParameter);

            if (result.Code == 0)
            {
                _database.Save();
                ClearItem();
            }

            GetAllProducts();
        }

        private void ClearItem()
        {
            txbId.Text = txbName.Text = txbUnitPrice.Text = txbDescription.Text = string.Empty;
            lbId.Visibility = txbId.Visibility = Visibility.Hidden;
            btnProductAdd.IsEnabled = true;
            btnProductDel.IsEnabled = btnProductUpd.IsEnabled = false;
        }

        private void btnProductUpd_Click(object sender, RoutedEventArgs e)
        {
            ProductParameter productParameter = new()
            {
                ProductId = new Guid(txbId.Text),
                Name = txbName.Text,
                UnitPrice = txbUnitPrice.Text,
                Description = txbDescription.Text
            };
            Result result = _updateProductService.Execute(productParameter);
            if (result.Code == 0)
            {
                _database.Save();
            }
            GetAllProducts();
        }
    }
}