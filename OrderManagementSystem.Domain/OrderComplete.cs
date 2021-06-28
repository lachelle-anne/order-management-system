using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain
{
    /// <summary>
    /// order complete state
    /// </summary>
    public class OrderComplete : OrderState
    {
        /// <summary>
        /// connects the base of order state to order completed
        /// </summary>
        /// <param name="orderHeader">order id</param>
        public OrderComplete(OrderHeader orderHeader) : base(orderHeader)
        {

        }

        /// <summary>
        /// gets the orderstate which is completed
        /// </summary>
        public override OrderStates State { get => OrderStates.Complete; } 

        /// <summary>
        /// catches the exception when the order state changes
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Complete(ref OrderState state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// catches the exception when the order state changes
        /// </summary>
        /// <param name="state">the state in which the order is in</param>
        public override void Reject(ref OrderState state)
        {
            throw new NotImplementedException();
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
