using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain
{
    /// <summary>
    /// setting the order states
    /// </summary>
    public enum OrderStates { New = 1, Pending = 2, Rejected = 3, Complete = 4}

    public abstract class OrderState
    {
        /// <summary>
        /// order header field
        /// </summary>
        protected OrderHeader _orderHeader;

        /// <summary>
        /// order state within the order header
        /// </summary>
        /// <param name="orderHeader">the order header</param>
        public OrderState(OrderHeader orderHeader)
        {
            _orderHeader = orderHeader;
        }

        /// <summary>
        /// order state - submit
        /// </summary>
        /// <param name="state">state of the order</param>
        public abstract void Submit(ref OrderState state);
        /// <summary>
        /// order state - complete
        /// </summary>
        /// <param name="state">state of the order</param>
        public abstract void Complete(ref OrderState state);

        /// <summary>
        /// order state - reject
        /// </summary>
        /// <param name="state">state of the order</param>
        public abstract void Reject(ref OrderState state);

        /// <summary>
        /// get the order states from the state
        /// </summary>
        public abstract OrderStates State { get; }

    }
}
