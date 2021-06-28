using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain
{
    /// <summary>
    /// properties for the item in the order
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// initialising the parameters for the order item
        /// </summary>
        /// <param name="order">all properties within the order header</param>
        /// <param name="stockItemId">item identification</param>
        /// <param name="price">price of the item/s</param>
        /// <param name="description">description of the item/s</param>
        /// <param name="quantity">quantity available of the item/s</param>
        public OrderItem(OrderHeader order, int stockItemId, decimal price, string description, int quantity)
        {
            OrderHeaderId = order.Id;
            OrderHeader = order;
            StockItemId = stockItemId;
            Price = price;
            Description = description;
            Quantity = quantity;
        }
         
        /// <summary>
        /// get and set for the order header id
        /// </summary>
        public int OrderHeaderId { get; set; }

        /// <summary>
        /// get and set for the order header
        /// </summary>
        public  OrderHeader OrderHeader { get; set; }

        /// <summary>
        /// get and set for the stock item id
        /// </summary>
        public int StockItemId { get; set; }

        /// <summary>
        /// get and set for the item price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// get and set for the item description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// get and set for the item available quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// get for the total price by calculating the price times quantity of items
        /// </summary>
        public decimal Total { get => Price * Quantity; }
    }
}
