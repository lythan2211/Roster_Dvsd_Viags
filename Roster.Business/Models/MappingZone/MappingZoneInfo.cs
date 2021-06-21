using Roster.Data.Utils;
using System;
using Roster.Business.Extensions;
using System.Collections.Generic;
using System.Linq;
namespace Roster.Business.Models
{
    public class MappingZoneInfo
    {
        [DataConvert("Id")]
        public int Id { get; set; }

        [DataConvert("Zone")]
        public int Zone { get; set; }

        [DataConvert("Mapping")]
        public string Mapping { get; set; }

        public List<int> ZoneMapping { 
            get {
                if (this.Mapping.IsNullOrEmpty())
                {
                    return new List<int>();
                }
                else
                {
                    return this.Mapping.Split(',').Select(Int32.Parse).ToList();
                }
            }
        }

        public MappingZoneInfo()
        {

        }

        public MappingZoneInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, MappingZoneInfo>(srcObject, this);
            }
        }
    }
}
