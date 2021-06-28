using OrderManagementSystem.Controllers;
using OrderManagementSystem.Domain;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrderManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Page
    {
        public OrdersView()
        {
            InitializeComponent();

            //singleton pattern
            //var ctrl1 = OrderController.Instance;
            //int ctrl1hc = ctrl1.GetHashCode();

            //var ctrl2 = OrderController.Instance;
            //int ctrl2hc = ctrl1.GetHashCode();

            //var orders = ctrl1.GetOrderHeaders();
            try
            {
                dgOrders.ItemsSource = OrderController.Instance.GetOrderHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderView());
        }

        private void OrdersGridItem_Click(object sender, RoutedEventArgs e)
        {

            var button = (Button)e.Source;
            var order = (OrderHeader)button.DataContext;
            
            NavigationService.Navigate(new OrderDetailView(order));
        }
    }
}
