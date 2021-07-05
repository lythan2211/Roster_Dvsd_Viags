namespace Roster.Business.Models.TrucNhanSuTheoNgay
{
    public class ChuyenBayCatGiam
    {
       
        public string HangBay   { get; set; } 
        
        public string SoHieu { get; set; } 

        public string LoaiTau   { get; set; } 
        
        public string ChangBay { get; set; }

        public ChuyenBayCatGiam(string hangBay, string soHieu, string loaiTau, string changBay)
        {
            HangBay = hangBay;
            SoHieu = soHieu;
            LoaiTau = loaiTau;
            ChangBay = changBay;
        }
    }
}