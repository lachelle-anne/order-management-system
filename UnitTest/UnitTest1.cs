using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagementSystem.Controllers;
using OrderManagementSystem.Domain;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initialize()
        {
            OrderController.Instance.ResetDatabase();
        }

        [TestCleanup]
        public void CleanUp()
        {
            OrderController.Instance.ResetDatabase();
        }

        [TestMethod]
        public void TestStateChange_Complete()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            order = OrderController.Instance.UpsertOrderItem(order.Id, 1, 1);
            OrderController.Instance.SubmitOrder(order.Id);
            OrderController.Instance.ProcessOrder(order.Id);
            Assert.IsTrue(OrderStates.Complete > 0);
        }

        [TestMethod]
        public void TestStateChange_Rejected()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            order = OrderController.Instance.UpsertOrderItem(order.Id, 2, 11);
            OrderController.Instance.SubmitOrder(order.Id);
            OrderController.Instance.ProcessOrder(order.Id);
            Assert.IsTrue(OrderStates.Rejected > 0);
        }
    }
}
