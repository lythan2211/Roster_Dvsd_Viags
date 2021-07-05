using System.Collections.Generic;

namespace Roster.Business.Models.TrucNhanSuTheoNgay
{
    public class NhanSuCuaKip
    {
        public int SttKip   { get; set; } 
        
        public int minStart { get; set; } 
        
        public List<ThoiGianLamViecCuaNhanSu> ThoiGianLamViecCuaNhanSuDke    { get; set; } 
        
        public List<ThoiGianLamViecCuaNhanSu>   ThoiGianLamViecCuaNhanSuXn   { get; set; } 
        
        public List<ThoiGianLamViecCuaNhanSu>   ThoiGianLamViecCuaNhanSuBta  { get; set; } 
        
        public List<ThoiGianLamViecCuaNhanSu>   ThoiGianLamViecCuaNhanSuBx   { get; set; }

        public NhanSuCuaKip(int sttKip, int minStart, List<ThoiGianLamViecCuaNhanSu> thoiGianLamViecCuaNhanSuDke, List<ThoiGianLamViecCuaNhanSu> thoiGianLamViecCuaNhanSuXn, List<ThoiGianLamViecCuaNhanSu> thoiGianLamViecCuaNhanSuBta, List<ThoiGianLamViecCuaNhanSu> thoiGianLamViecCuaNhanSuBx)
        {
            SttKip = sttKip;
            this.minStart = minStart;
            ThoiGianLamViecCuaNhanSuDke = thoiGianLamViecCuaNhanSuDke;
            ThoiGianLamViecCuaNhanSuXn = thoiGianLamViecCuaNhanSuXn;
            ThoiGianLamViecCuaNhanSuBta = thoiGianLamViecCuaNhanSuBta;
            ThoiGianLamViecCuaNhanSuBx = thoiGianLamViecCuaNhanSuBx;
        }

        public NhanSuCuaKip()
        {
            throw new System.NotImplementedException();
        }
    }
}