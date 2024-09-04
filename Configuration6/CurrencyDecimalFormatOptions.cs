using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration6
{
    public class CurrencyDecimalFormatOptions
    {
        /// <summary>
        /// 数字
        /// </summary>
        public int Digits { get; set; }

        /// <summary>
        /// 符号
        /// </summary>
        public string Symbol { get; set; }

        //public CurrencyDecimalFormatOptions(IConfiguration config) 
        //{
        //    Digits = int.Parse(config["Digits"]);
        //    Symbol = config["Symbol"];
        //}
    }
}
