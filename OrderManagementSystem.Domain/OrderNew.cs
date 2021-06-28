using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain
{
    /// <summary>
    /// order new state
    /// </summary>
    public class OrderNew : OrderState
    {
        /// <summary>
        /// connects the base of order state to new order 
        /// </summary>
        /// <param name="orderHeader">order id</param>
        public OrderNew(OrderHeader orderHeader) : base(orderHeader)
        {

        }

        /// <summary>
        /// gets the orderstate which is new
        /// </summary>
        public override OrderStates State { get => OrderStates.New; }


        /// <summary>
        /// catches the exception when the order state changes
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Complete(ref OrderState state)
        {
            throw new NotImplementedException("A new order must first be submitted (pending) before it can be completed");
        }

        /// <summary>
        /// catches the exception when the order state changes
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Reject(ref OrderState state)
        {
            throw new NotImplementedException("A new order must first be submitted (pending) before it can be rejected");
        }

        /// <summary>
        /// catches the exception when the order state changes
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Submit(ref OrderState state)
        {
            //an order must at least have one order item
            if (!_orderHeader.OrderItems.Any())
            {
               throw new InvalidOperationException("A new order must at least have one item before it can be submitted");
            }

            //change to state pending
            state = new OrderPending(_orderHeader);
        }
    }
}
