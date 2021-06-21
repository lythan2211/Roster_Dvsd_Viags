using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System;
using System.Collections.Generic;

namespace Roster.Business.DAO
{
    public interface IProductRepository : IRepository<TBL_ProductList>
    {
        /// <summary>
        /// Get an IEnumerable TBL_ProductList 
        /// </summary>
        /// <returns><c>IEnumerable TBL_ProductList</c> if TBL_ProductList not Empty, <c>null</c> otherwise</returns>
        IEnumerable<TBL_ProductList> Filter(List<string> productCode);

    }
}
