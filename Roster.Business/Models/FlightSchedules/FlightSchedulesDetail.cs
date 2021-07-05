using System.Collections.Generic;

namespace Roster.Business.Models
{
    public class FlightSchedulesDetail
    {
        public int Id { get; set; }

        public string FlightNo { get; set; }        // so hieu chuyen bay
        
        public string ACCode { get; set; }          // ma cua hang may bay AIRLINE_COMPANY_CODE
        
        public string ArrNo { get; set; }            // so hieu den
        
        public string DepNo { get; set; }            // so hieu di
        
        public int ArrDep { get; set; }             // den+di=1, di = 2, den = 3
        
        public string AirCraft { get; set; }        // loai may bay
        
        public string AirCraftStd { get; set; }     // loai may bay sau khi da chuan hoa
        
        public string Route { get; set; }           // duong di
        
        public string ArrQtqn { get; set; }         // quoc te, quoc noi chuyen den
        
        public string DepQtqn { get; set; }         // quoc te, quoc noi chuyen di
        
        public string DomInt { get; set; }          // quoc te - noi dia. duoc suy ra tu ArrQtnt + DepQtnt
        
        public string ArrRemark { get; set; }       // tinh chat chuyen cho chuyen den
        
        public string DepRemark { get; set; }       // tinh chat chuyen cho chuyen di
            
        public string Carry { get; set; }           // tinh chat chuyen cho. duoc suy ra tu ArrRemark + DepRemark
        
        public string ETA { get; set; }             // thoi gian ket thuc den
        
        public string STA { get; set; }             // thoi gian bat dau den
        
        public string ETD { get; set; }             // thoi gian ket thuc di
        
        public string STD { get; set; }             // thoi gian bat dau di
        
        public string FlightDay { get; set; }       // ngay bay
        
        public string Register        { get; set; } // dang ky tau
        
        public int    Zone            { get; set; } // vung do xe, duoc suy ra tu parking code
        
        public List<ProductDetail> ProductLists    { get; set; } // product list get from db
        
        public int    turnAroundTime  { get; set; } // precalculate turn around time for ArrDep = 1
        
        public int    minDoneServe    { get; set; } 
        
        public int    dayIdxDoneServe { get; set; } 
        
        public string FK_sEmpID_Kip   { get; set; } // ID KIP TRUONG
        
        public string SClose		  { get; set; } // sclone = 1 CHUYEN BAY CHUYEN CO
        
        public int    minStart        { get; set; } 
        
        public int    minEnd          { get; set; } 
        
        public int    countDKEXNG     { get; set; } 
        
        public bool   kiemTraCatGiam  { get; set; } 
        
        public FlightSchedulesDetail()
        { 

        }
        
    }
}