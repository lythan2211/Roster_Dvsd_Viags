namespace Roster.Business.Models.TrucNhanSuTheoNgay
{
    public class ThoiGianLamViecCuaNhanSu
    {
        public int NhanSu          { get; set; } 
        
        public int ThoiGianBatDau  { get; set; } 
        
        public int ThoiGianKetThuc { get; set; }

        public ThoiGianLamViecCuaNhanSu(int nhanSu, int thoiGianBatDau, int thoiGianKetThuc)
        {
            NhanSu = nhanSu;
            ThoiGianBatDau = thoiGianBatDau;
            ThoiGianKetThuc = thoiGianKetThuc;
        }
    }
}  