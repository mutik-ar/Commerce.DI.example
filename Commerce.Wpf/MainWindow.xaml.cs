using Model.Parameters;
using Model.CommandsServices;
using Model.Interfaces;
using Model.Results;
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
using Model.Entities;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Commerce.Wpf
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        IUnitOfWork _database;
        ICommandService<InsertProductParameter, Result> _insertProductService;
        ICommandService<NullParameter, GetAllProductsResult> _getAllProductService;
        public IEnumerable<Product> Products { get {return _products ;} set {_products= (List<Product>)value; OnPropertyChanged("Products"); } }
        List<Product> _products = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = this;
            _database = new UnitOfWork(@"Server=(localdb)\mssqllocaldb;Database=Commerce;Trusted_Connection=True");
            _insertProductService = new InsertProductService(_database.Products);
            _getAllProductService = new GetAllProductService(_database.Products);
            GetAllProducts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertProductParameter insertProductParameter = new()
            {
                ProductId = Guid.NewGuid(),
                Name = txbName.Text,
                UnitPrice = txbUnitPrice.Text,
                Description = txbDescription.Text
            };

            Result result = _insertProductService.Execute(insertProductParameter);

            if (result.Code == 0)
            {
                _database.Save();
            }

            GetAllProducts();
        }

        private void RefreshProduct_Click(object sender, RoutedEventArgs e)
        {
            GetAllProducts();
        }

        private void GetAllProducts()
        {
            IEnumerable<Product> products = _getAllProductService.Execute(new NullParameter()).Products;
            if (products != null)
            {
                Products = products;
            }
        }
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}