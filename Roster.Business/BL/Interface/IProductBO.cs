using Roster.Business.Models;
using System;
using System.Collections.Generic;

namespace Roster.Business.BL
{
    public interface IProductBO
    {
        /// <summary>
        /// Search EmailActiveInfo by condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        List<ProductInfo> Filter(List<string> productCode);
    }
}
