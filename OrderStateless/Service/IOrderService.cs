using OrderStateless.Models;

namespace OrderStateless.Service
{
    public interface IOrderService
    {
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order GetOrder(int id);

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        void CreateOrder(Order order);

        /// <summary>
        /// 支付订单
        /// </summary>
        /// <param name="id"></param>
        void PayOrder(int id);

        /// <summary>
        /// 发货订单
        /// </summary>
        /// <param name="id"></param>
        void ShipOrder(int id);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="id"></param>
        void CompleteOrder(int id);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        void CancelOrder(int id);
    }
}
