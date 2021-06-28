using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain
{
    /// <summary>
    /// order state pending
    /// </summary>
    public class OrderPending : OrderState
    {
        /// <summary>
        /// connects the base of order state to order pending
        /// </summary>
        /// <param name="orderHeader">order id</param>
        public OrderPending(OrderHeader orderHeader) : base(orderHeader) { }

        /// <summary>
        /// gets the orderstate which is pending
        /// </summary>
        public override OrderStates State => OrderStates.Pending;

        /// <summary>
        /// created the new order state rejected
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Complete(ref OrderState state)
        {
            state = new OrderComplete(_orderHeader);
        }

        /// <summary>
        /// created the new order state rejected
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Reject(ref OrderState state)
        {
            state = new OrderRejected(_orderHeader);
        }

        /// <summary>
        /// catches the exception when the order state changes
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Submit(ref OrderState state)
        {
            throw new NotImplementedException();
        }
    }
}
