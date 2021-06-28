using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.DAL
{
    /// <summary>
    /// Contains the data for the stock repository 
    /// </summary>
    public class StockRepo
    {

        string connectionString = "Data Source= SABER\\SQLEXPRESS; Initial Catalog=OrderManagementDb;Integrated Security=true";

        /// <summary>
        /// talks to the database and get all the stock items and places it into a list
        /// </summary>
        /// <returns>items</returns>
        public List<StockItem> GetStockItems()
        {
            var items = new List<StockItem>();

            using (var connection = new SqlConnection(connectionString))
            using(var command = new SqlCommand("sp_SelectStockItems", connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    int inStock = reader.GetInt32(3);
                    var item = new StockItem(id, name, price, inStock);
                    items.Add(item);
                }
            }
                //get data from database and create ist of stock item objects
                return items;
        }

        /// <summary>
        /// talks to the database to retrieve the stock details through the id
        /// </summary>
        /// <param name="id">The identification number of the stock item</param>
        /// <returns>item</returns>
        public StockItem GetStockItem(int id)
        {
            StockItem item = null;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_SelectStockItemById @id", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                reader.Read();
                string name = reader.GetString(1);
                decimal price = reader.GetDecimal(2);
                int inStock = reader.GetInt32(3);
                item = new StockItem(id, name, price, inStock);
            }
            return item;
        }

        /// <summary>
        /// checks that the item quantity deducts for each order item
        /// </summary>
        /// <param name="order">The order created </param>
        public void DecrementOrderStockItemAmount(OrderHeader order)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_UpdateStockItemAmount @id, @amount", connection))
            {
                connection.Open();
                //transacrion object
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    //loop through the order for each order item
                    foreach(var oi in order.OrderItems)
                    {
                        command.Parameters.AddWithValue("@id", oi.StockItemId);
                        command.Parameters.AddWithValue("@amount", -oi.Quantity);
                        command.ExecuteNonQuery();
                        //clears the parameters ready for the next item
                        command.Parameters.Clear();
                    }
                    //commits the transaction to change the quantity within the database
                    transaction.Commit();
                }
                catch(SqlException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
