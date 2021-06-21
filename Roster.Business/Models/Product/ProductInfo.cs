using Roster.Data.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Roster.Business.Models
{
    public class ProductInfo
    {
        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ShortName { get; set; }

        public int StartTime { get; set; }

        public int DuringTime { get; set; }

        public int ManQuota { get; set; }

        public ProductInfo()
        {

        }

        public ProductInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ProductInfo>(srcObject, this);
            }
        }


    }
}
