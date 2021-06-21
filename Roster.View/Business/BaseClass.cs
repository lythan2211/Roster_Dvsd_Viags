using Roster.Business.BL;
using Roster.Data.DBAccessor;
using Roster.Business.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster.Business
{
    public class BaseClass
    {
        private readonly IDbContext dbContext = DbContextManager.GetContext();
        protected IBOFactory GetBOFactory()
        {
            return new BOFactory(this.dbContext);
        }

        protected IBOFactory GetBOFactory(string nameOrSqlConnectionString)
        {
            return new BOFactory(DbContextManager.GetContext(nameOrSqlConnectionString));
        }


        ///Mapping Zone Code
        ///Zone 1: Vị trí cầu T1: từ 15 đến 21
        //Zone 2: Vị trí cầu T2 đầu Tây: từ 30 đến 39
        //Zone 3: Vị trí cầu T2 đầu Đông: từ 40 đến 51
        //Zone 4: Ô 15: từ 71 đến 86
        //Zone 5: Các vị trí 1H, 2H, 3H và từ 1 đến 4
        //Zone 6: Từ 5 đến 14
        //Zone 7: Từ 20A đến 29A
        //Zone 8: Từ 52B đến 58
        ///End Mapping Zone 

        public int GetZone(string pakkingCode)
        {
            if (pakkingCode.IsNullOrEmpty())
            {
                return 0;
            }

            if (pakkingCode.Contains("H") || pakkingCode.Contains("h"))
            {
                return 5;
            }

            if (pakkingCode.Contains("A") || pakkingCode.Contains("a"))
            {
                return 7;
            }

            if (pakkingCode.Contains("B") || pakkingCode.Contains("b"))
            {
                return 8;
            }

            if (pakkingCode.ToInt(0) >= 1 && pakkingCode.ToInt(0) <= 4)
            {
                return 5;
            }
            else if (pakkingCode.ToInt(0) >= 5 && pakkingCode.ToInt(0) <= 14)
            {
                return 6;
            }
            else if (pakkingCode.ToInt(0) >= 15 && pakkingCode.ToInt(0) <= 21)
            {
                return 1;
            }
            else if (pakkingCode.ToInt(0) >= 30 && pakkingCode.ToInt(0) <= 39)
            {
                return 2;
            }
            else if (pakkingCode.ToInt(0) >= 40 && pakkingCode.ToInt(0) <= 51)
            {
                return 3;
            }
            else if (pakkingCode.ToInt(0) >= 71 && pakkingCode.ToInt(0) <= 86)
            {
                return 4;
            }
            else if (pakkingCode.ToInt(0) >= 20 && pakkingCode.ToInt(0) <= 29)
            {
                return 7;
            }
            else if (pakkingCode.ToInt(0) >= 52 && pakkingCode.ToInt(0) <= 58)
            {
                return 8;
            }

            return 0;
        }
    }
}
