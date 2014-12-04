using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///
/// 订单操作类
///
namespace ZK.Common
{
    public class OrderHelper
    {
        /// <summary>
        /// 生成制卡订单号
        /// </summary>
        /// <returns></returns>
        public static string BuildBurdCardOrder()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyyMMddHHmmss") + now.Millisecond.ToString() + now.Ticks.ToString();
        }

        /// <summary>
        /// 生成申领订单号
        /// </summary>
        /// <returns></returns>
        public static string BuildApplyCardOrder()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyyMMddHHmmss") + now.Millisecond.ToString() + now.Ticks.ToString();
        }
    }
}
