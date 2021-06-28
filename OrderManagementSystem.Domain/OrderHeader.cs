using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain
{
    /// <summary>
    /// properties and constructors of the order header
    /// </summary>
    public class OrderHeader
    {
        /// <summary>
        /// private state field
        /// </summary>
        private OrderState _state;

        /// <summary>
        /// constructor for the order header
        /// </summary>
        /// <param name="id">id of the order</param>
        /// <param name="dateTime">date/time of the order</param>
        /// <param name="stateId">state of the order</param>
        public OrderHeader(int id, DateTime dateTime, int stateId)
        {
            Id = id;
            DateTime = dateTime;
            setState(stateId);
        }

        /// <summary>
        /// List of order items for a new order
        /// </summary>
        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();

        /// <summary>
        /// adds or updates the order item for each item
        /// </summary>
        /// <param name="stockItemId">item id of the stock</param>
        /// <param name="price">price of the item/s</param>
        /// <param name="description">description of the item/s</param>
        /// <param name="quantity">quantity of item/s</param>
        /// <returns></returns>
        public OrderItem AddOrUpdateOrderItem(int stockItemId, decimal price, string description, int quantity)
        {
            OrderItem item = null;

            //check to see if there is already am existing order item for the selected stock item
            foreach (var i in OrderItems)
            {
                if (i.StockItemId == stockItemId)
                {
                    item = i;
                }
            }
            //if there isn't, create a new instance and add it to the collection of order items
            if (item == null)
            {
                item = new OrderItem(this, stockItemId, price, description, quantity);
                OrderItems.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            //return the order item object
            return item;

        }

        /// <summary>
        /// get and set the id property
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// get and set the id property
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// get for the stateId property
        /// </summary>
        public int StateId { get => (int)_state.State; }

        /// <summary>
        /// get for the state property
        /// </summary>
        public OrderStates State { get => _state.State; }

        /// <summary>
        /// get for the total sum of order items
        /// </summary>
        public decimal Total { get => OrderItems.Sum(i=>i.Total); } 

        /// <summary>
        /// method to set the state
        /// </summary>
        /// <param name="stateId">state correlating with the id</param>
        private void setState(int stateId)
        {
            switch (stateId)
            {
                case 1:
                    _state = new OrderNew(this);
                    break;
                case 2:
                    _state = new OrderPending(this);
                    break;
                case 3:
                    _state = new OrderRejected(this);
                    break;
                case 4:
                    _state = new OrderComplete(this);
                    break;
                default:
                    throw new InvalidOperationException("Invalid StateId Detected");
            }
        }

        /// <summary>
        /// updates the state of the order - Submit[New > Pending]
        /// </summary>
        public void Submit()
        {
            _state.Submit(ref _state);
        }

        /// <summary>
        /// updates the state of the order - Complete[Pending > Complete]
        /// </summary>
        public void Complete()
        {
            _state.Complete(ref _state);
        }

        /// <summary>
        /// updates the state of the order - Reject[Pending > Rejected]
        /// </summary>
        public void Reject()
        {
            _state.Reject(ref _state);
        }

    }
}
