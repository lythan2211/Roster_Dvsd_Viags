using Roster.Business.DAO;
using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System;
using System.Collections.Generic;
using Roster.Business.Extensions;
using System.Linq;
using Roster.Common;

namespace Roster.Business.BL
{
    public class ProductBO : IProductBO
    {
        #region Fields, Properties

        private readonly IProductRepository productRepository;
        #endregion

        #region Contructor

        public ProductBO(IRepositoryFactory repoFactory)
        {
            this.productRepository = repoFactory.GetRepository<IProductRepository>();
        }

        #endregion

        #region Methods
        public List<ProductInfo> Filter(List<string> productCode)
        {
            var products = this.productRepository.Filter(productCode);
            return products.Select(p => new ProductInfo(p)).ToList();
        }

        #endregion
        
    }
}