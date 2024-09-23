using OrderStateless.Models;
using Stateless;
using System;
using System.Collections.Generic;

namespace OrderStateless.Service
{
    public class OrderService : IOrderService
    {
        private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();

        private int _nextId = 1;

        public Order GetOrder(int id) => _orders.GetValueOrDefault(id);

        public void CreateOrder(Order order)
        {
            order.Id = _nextId++;
            _orders[order.Id] = order;
        }

        public void PayOrder(int id) => UpdateOrderState(id, OrderState.Paid);
        public void ShipOrder(int id) => UpdateOrderState(id, OrderState.Shipped);
        public void CompleteOrder(int id) => UpdateOrderState(id, OrderState.Completed);
        public void CancelOrder(int id) => UpdateOrderState(id, OrderState.Canceled);

        private void UpdateOrderState(int id, OrderState newState)
        {
            if (!_orders.ContainsKey(id)) throw new Exception("Order not found");

            var order = _orders[id];
            var stateMachine = new StateMachine<OrderState, string>(order.State);

            // 待办：付款or取消
            stateMachine.Configure(OrderState.Pending)
                .Permit("Pay", OrderState.Paid)
                .Permit("Cancel", OrderState.Canceled);

            //付款：
            stateMachine.Configure(OrderState.Paid)
                .Permit("Ship", OrderState.Shipped)
                .Permit("Cancel", OrderState.Canceled);

            stateMachine.Configure(OrderState.Shipped)
                .Permit("Complete", OrderState.Completed);

            //完成变成取消
            stateMachine.Configure(OrderState.Completed)
               .Permit("Cancel", OrderState.Canceled);

            // Fire the appropriate trigger
            switch (newState)
            {
                case OrderState.Paid:
                    stateMachine.Fire("Pay");
                    break;
                case OrderState.Shipped:
                    stateMachine.Fire("Ship");
                    break;
                case OrderState.Completed:
                    stateMachine.Fire("Complete");
                    break;
                case OrderState.Canceled:
                    stateMachine.Fire("Cancel");
                    break;
            }

            order.State = stateMachine.State;
        }
    }
}
