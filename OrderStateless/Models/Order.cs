using System;

namespace OrderStateless.Models
{
    public class Order
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        public OrderState State { get; set; } = OrderState.Pending;
    }
    public enum OrderState
    {
        Pending, //代办
        Paid,    //已付款
        Shipped, //已发货
        Completed, //已完成
        Canceled  //已取消
    }
}
