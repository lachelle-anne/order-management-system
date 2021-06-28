using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.DAL;
using OrderManagementSystem.Domain;
using System.Data.SqlClient;

namespace OrderManagementSystem.Controllers
{
    public class OrderController
    {
        //fields
        private readonly OrderRepo _orderRepo = new OrderRepo();
        private readonly StockRepo _stockRepo = new StockRepo();

        //singleton pattern
        private static OrderController _instance;
        private OrderController() { }
        public static OrderController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new OrderController();
                }
                return _instance;
            }
        }

        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            return _orderRepo.GetOrderHeaders();
        }

        public OrderHeader CreateNewOrderHeader()
        {
            return _orderRepo.InsertOrderHeader();
        }

        public OrderHeader UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity)
        {
            //find the associated stock item object by id
            var stockItem = _stockRepo.GetStockItem(stockItemId);
            //find the associated order header object
            var order = _orderRepo.GetOrderHeader(orderHeaderId);
            //create or update the order item object
            var orderItem = order.AddOrUpdateOrderItem(stockItemId, stockItem.Price, stockItem.Name, quantity);
            //persist the changes to the database
            _orderRepo.UpsertOrderItem(orderItem);
            //return the updated order
            return order;
        }

        public OrderHeader DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            _orderRepo.DeleteOrderItem(orderHeaderId, stockItemId);
            var order = _orderRepo.GetOrderHeader(orderHeaderId);
            return order;
        }

        public OrderHeader SubmitOrder(int orderHeaderId)
        {
            // create (retrieve) a new (fresh) instance of the order object
            var order = _orderRepo.GetOrderHeader(orderHeaderId);
            //update the state of the order pending (in memory i.e. RAM)
            order.Submit();
            //persist the changes to the database
            _orderRepo.UpdateOrderState(order);
            //return the updated instance of the order object
            return order;
        }

        public OrderHeader ProcessOrder(int orderHeaderId)
        {
            var order = _orderRepo.GetOrderHeader(orderHeaderId);
            try
            {
                //if the stock item amount can be decreased within the set parameters, complete the order
                _stockRepo.DecrementOrderStockItemAmount(order);
                order.Complete();
            }
            catch(SqlException ex)
            {
                int exceptionNumber = ex.Number; //547 - SQL check constraint error message
                //this means the stock item check constraint threw an exception
                order.Reject();
            }
            _orderRepo.UpdateOrderState(order);
            return order;
        }

        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            _orderRepo.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }

        public void ResetDatabase()
        {
            _orderRepo.ResetDatabase();
        }
    }
}
