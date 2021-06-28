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
    /// Interaction logic for AddOrderItemView.xaml
    /// </summary>
    public partial class AddOrderItemView : Page
    {
        private OrderHeader _order;

        public IEnumerable<StockItem> StockItems { get; private set; }
        public int Quantity { get; set; } = 1;
        public AddOrderItemView(OrderHeader order)
        {
            InitializeComponent();
            try
            {
                _order = order;
                StockItems = StockItemController.Instance.GetStockItems();
                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (StockItem)dgStockItems.SelectedItem;
            if(item == null)
            {
                MessageBox.Show("Please select a stock item", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (Quantity < 1)
            {
                MessageBox.Show("Quantity must be greater than zero (0)", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (Quantity > item.InStock)
            {
                MessageBox.Show($"QUANTITY SELECTED: {Quantity} \n There is currently {item.InStock} item/s in stock.\n This order may be rejected if you choose to proceed with a higher quantity than in stock", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                var order = OrderController.Instance.UpsertOrderItem(_order.Id, item.Id, Quantity);
                NavigationService.Navigate(new AddOrderView(order));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
