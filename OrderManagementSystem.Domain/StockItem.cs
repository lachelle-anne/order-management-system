using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain
{
    /// <summary>
    /// stock item properties
    /// </summary>
    public class StockItem
    {

        /// <summary>
        /// initialising the parameters for the stock item
        /// </summary>
        /// <param name="id">stock item id</param>
        /// <param name="name">strock item name</param>
        /// <param name="price">stock item price</param>
        /// <param name="inStock">in stock item</param>
        public StockItem(int id, string name, decimal price, int inStock)
        {
            Id = id;
            Name = name;
            Price = price;
            InStock = inStock;
        }

        /// <summary>
        /// get and set for item id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// get and set for item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// get and set for item price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// get and set for in stock property
        /// </summary>
        public int InStock { get; set; }

    }
}
