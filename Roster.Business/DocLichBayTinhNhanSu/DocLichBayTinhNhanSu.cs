using System;
using System.Collections.Generic;
using System.Linq;
using Roster.Business.Extensions;
using Roster.Business.Models;
using Roster.Business.Models.TrucNhanSuTheoNgay;

namespace Roster.Business.DocLichBayTinhNhanSu
{
    public class DocLichBayTinhNhanSu
    {
        private List<ChuyenBayCatGiam> danhSachChuyenBayCatGiamDkeXn;
        private List<ChuyenBayCatGiam> danhSachChuyenBayCatGiamDke15P;
        private List<ChuyenBayCatGiam> danhSachChuyenBayCatGiamBta;
        private List<FlightSchedulesCargo> danhSachMaChuyenBayCargo;
        private List<MuiGio> danhSachMuiGioTrongNgay;
        private FlightSchedulesDetail danhSachLichBayTrongNgay;

        public DocLichBayTinhNhanSu()
        {
            LayDanhSachLoaiChuyenBayCatGiam();
            CaiDatMuiGio();
        }

        public void DocLichBayNgayTinhNhanSu(DateTime ngayCanTinh, List<FlightSchedulesDetail> danhSachLichBayTrongNgay)
        {
            List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay = KhoiTaoTrucNhanSuTheoNgay();

            danhSachLichBayTrongNgay = CatGiamNhanSuChuyenBay(danhSachLichBayTrongNgay);
            
            foreach (FlightSchedulesDetail chuyenBay in danhSachLichBayTrongNgay)
            {
                trucNhanSuTheoNgay = ChuyenBayTrucNhanSu(chuyenBay, trucNhanSuTheoNgay);
            }

            trucNhanSuTheoNgay = GiamNhanSuTaiTrucThoiGian(trucNhanSuTheoNgay);

            List<TrucNhanSuTheoNgay> trucNhanSuMaxNsTungMuiGio = TinhNhanSuTheoGio(trucNhanSuTheoNgay);
            
            List<NhanSuCuaKip> nhanSuKipBuoiSang = LayNhanSuCaBuoiSang(trucNhanSuTheoNgay);
          
            List<NhanSuCuaKip> nhanSuKipBuoiChieu = LayNhanSuCaChieu(trucNhanSuTheoNgay,nhanSuKipBuoiSang);
            
            // LUU XUONG DB

        }

        public List<TrucNhanSuTheoNgay> KhoiTaoTrucNhanSuTheoNgay()
        {
            List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay = new List<TrucNhanSuTheoNgay>();
            for(int i = 1 ; i <= 2160; i++ ) {
                TrucNhanSuTheoNgay trucNhanSuTungPhut =  new TrucNhanSuTheoNgay();
                trucNhanSuTungPhut.Phut = i;
                trucNhanSuTungPhut.NhanSuBta = 0;
                trucNhanSuTungPhut.NhanSuBx = 0;
                trucNhanSuTungPhut.NhanSuDke = 0;
                trucNhanSuTungPhut.NhanSuXn = 0;
                trucNhanSuTheoNgay.Add(trucNhanSuTungPhut);
            }
            return trucNhanSuTheoNgay;
        }

        public List<FlightSchedulesDetail> CatGiamNhanSuChuyenBay(List<FlightSchedulesDetail> danhSachLichBayTrongNgay) {
            for (int i = 0; i < danhSachLichBayTrongNgay.Count; i++)
            {
                if (kiemTraChuyenBayCoCatGiamKhong(danhSachLichBayTrongNgay[i], danhSachChuyenBayCatGiamDkeXn)) {
                    danhSachLichBayTrongNgay[i] = LenhCatGiamDkeXnChuyenBay(danhSachLichBayTrongNgay[i]);
                }
                if (kiemTraChuyenBayCoCatGiamKhong(danhSachLichBayTrongNgay[i], danhSachChuyenBayCatGiamBta)) {
                    danhSachLichBayTrongNgay[i] = LenhCatGiamBTAChuyenBay(danhSachLichBayTrongNgay[i]);
                } 
            }
            return danhSachLichBayTrongNgay;
        }
        
        public bool kiemTraChuyenBayCoCatGiamKhong(FlightSchedulesDetail thongTinChuyenBay, List<ChuyenBayCatGiam> danhSachChuyenBayCatGiam){
            foreach (ChuyenBayCatGiam cbCatGiam in danhSachChuyenBayCatGiam) {
                if (thongTinChuyenBay.FlightNo == cbCatGiam.SoHieu && thongTinChuyenBay.AirCraft == cbCatGiam.LoaiTau &&
                    thongTinChuyenBay.Route == cbCatGiam.ChangBay && thongTinChuyenBay.ACCode == cbCatGiam.HangBay)
                {
                    return true;
                }
            }
            return false;
        }

        public void LayDanhSachLoaiChuyenBayCatGiam()
        {
            danhSachChuyenBayCatGiamDkeXn = new List<ChuyenBayCatGiam>();
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN417","A350","ICN-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN417","B787","ICN-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN427","A350","PUS-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN417","B787","PUS-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN311","A350","NRT-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN311","B787","NRT-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN311","A350","KIX-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN311","B787","KIX-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN531","A350","PVG-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN531","B787","PVG-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN531","A350","PEK-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN531","B787","PEK-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN385","A350","HND-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN385","B787","HND-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN415","A350","ICN-HAN"));
            danhSachChuyenBayCatGiamDkeXn.Add(new ChuyenBayCatGiam("VN","VN415","B787","ICN-HAN"));
            
            danhSachChuyenBayCatGiamDke15P = new List<ChuyenBayCatGiam>();
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN230","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN232","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN236","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN230","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN242","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN248","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN284","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN7248","A321","SGN-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN154","A321","DAD-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN168","A321","DAD-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN172","A321","DAD-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN1650","A321","TTB-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN1624","A321","UIH-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN1542","A321","HUI-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN1548","A321","HUI-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN1708","A321","VII-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN1712","A321","VII-HAN"));
            danhSachChuyenBayCatGiamDke15P.Add(new ChuyenBayCatGiam("VN","VN1714","A321","VII-HAN"));
            
            danhSachChuyenBayCatGiamBta = new List<ChuyenBayCatGiam>();
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN154","A321","DAD-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN168","A321","DAD-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN172","A321","DAD-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN1650","A321","TTB-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN1624","A321","UIH-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN1542","A321","HUI-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN1548","A321","HUI-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN1708","A321","VII-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN1712","A321","VII-HAN"));
            danhSachChuyenBayCatGiamBta.Add(new ChuyenBayCatGiam("VN","VN1714","A321","VII-HAN"));
           
        }

        public void CaiDatMuiGio()
        {
            danhSachMuiGioTrongNgay = new List<MuiGio>();
            danhSachMuiGioTrongNgay.Add(new MuiGio(300,359));
            danhSachMuiGioTrongNgay.Add(new MuiGio(360,419));
            danhSachMuiGioTrongNgay.Add(new MuiGio(420,479));
            danhSachMuiGioTrongNgay.Add(new MuiGio(480,539));
            danhSachMuiGioTrongNgay.Add(new MuiGio(540,599));
            danhSachMuiGioTrongNgay.Add(new MuiGio(600,659));
            danhSachMuiGioTrongNgay.Add(new MuiGio(660,719));
            danhSachMuiGioTrongNgay.Add(new MuiGio(720,779));
            danhSachMuiGioTrongNgay.Add(new MuiGio(780,839));
            danhSachMuiGioTrongNgay.Add(new MuiGio(840,899));
            danhSachMuiGioTrongNgay.Add(new MuiGio(900,959));
            danhSachMuiGioTrongNgay.Add(new MuiGio(960,1019));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1020,1079));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1080,1139));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1140,1199));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1200,1259));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1260,1319));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1320,1379));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1380,1439));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1440,1499));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1500,1559));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1560,1619));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1620,1679));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1680,1739));
            danhSachMuiGioTrongNgay.Add(new MuiGio(1740,1800));
        }

        public FlightSchedulesDetail LenhCatGiamDkeXnChuyenBay(FlightSchedulesDetail chuyenBay)
        {
            for (int i = 0; i < chuyenBay.ProductLists.Count; i++)
            {
                if (chuyenBay.ProductLists[i].ShortName == "DKE")
                {
                    chuyenBay.ProductLists[i].ManQuota = chuyenBay.ProductLists[i].ManQuota - 1;
                }  
                if (chuyenBay.ProductLists[i].ShortName == "XN")
                {
                    chuyenBay.ProductLists[i].ManQuota = chuyenBay.ProductLists[i].ManQuota - 1;
                }  
            }
            return chuyenBay;
        }
        
        public FlightSchedulesDetail LenhCatGiamBTAChuyenBay(FlightSchedulesDetail chuyenBay)
        {
            for (int i = 0; i < chuyenBay.ProductLists.Count; i++)
            {
                if (chuyenBay.ProductLists[i].ShortName == "BTA")
                {
                    chuyenBay.ProductLists[i].ManQuota = chuyenBay.ProductLists[i].ManQuota - 1;
                }  
            }
            return chuyenBay;
        }

        public bool XacDinhChuyenCargoDen(string fliNo)
        {
            for (int i = 0; i < danhSachMaChuyenBayCargo.Count; i++)
            {
                if (danhSachMaChuyenBayCargo[i].ArrFlight == fliNo)
                {
                    return true;
                }
            }
            return false;
        }

        public bool XacDinhChuyenCargoDi(string fliNo)
        {
            for (int i = 0; i < danhSachMaChuyenBayCargo.Count; i++)
            {
                if (danhSachMaChuyenBayCargo[i].DepFlight == fliNo)
                {
                    return true;
                }
            }
            return false;
        }

        public List<TrucNhanSuTheoNgay> ChuyenBayTrucNhanSu(FlightSchedulesDetail chuyenBay, List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay)
        {
            int[] empCount = {0,0,0,0};          // SO LUONG NHAN VIEN YEU CAU   ( 0 : DAU KEO , 1 : XN, 2 : BTA, 4 : BX)
            int[] minStart = {0,0,0,0,0}; 
            int[] minEnd   = {0,0,0,0,0}; 
            // LAY SO LUONG NHAN VIEN VOI TUNG DINH NANG CUA CHUYEN BAY
            for (int i = 0; i < chuyenBay.ProductLists.Count; i++)
            {
                ProductDetail product = chuyenBay.ProductLists[i];
                if (product.ShortName == "DKE") {
                    empCount[0] = product.ManQuota;
                    int[] min = GetMinStartMinEndFLightExtensions.GetMinStartMinEnd(chuyenBay, product);
                    minStart[0] = min[0];
                    minEnd[0] = min[1];
                }
                if (product.ShortName == "XNM" || product.ShortName == "XNL" || product.ShortName == "XNG") {
                    empCount[1] = empCount[1] + product.ManQuota;
                    int[] min = GetMinStartMinEndFLightExtensions.GetMinStartMinEnd(chuyenBay, product);
                    minStart[1] = min[0];
                    minEnd[1] = min[1];
                }
                if (product.ShortName == "BTA") {
                    empCount[2] = product.ManQuota;
                    int[] min = GetMinStartMinEndFLightExtensions.GetMinStartMinEnd(chuyenBay, product);
                    minStart[2] = min[0];
                    minEnd[2] = min[1];
                }
                if (product.ShortName == "BXM" && product.ManQuota > 0 )
                {
                    empCount[3] = empCount[3] + product.ManQuota;
                    int[] min = GetMinStartMinEndFLightExtensions.GetMinStartMinEnd(chuyenBay, product);
                    minStart[3] = min[0];
                    minEnd[3] = min[1];
                    if  (XacDinhChuyenCargoDen(chuyenBay.FlightNo) || XacDinhChuyenCargoDi(chuyenBay.FlightNo))
                    {
                        empCount[3] = empCount[3] - 1;
                    }else{
                        if (chuyenBay.ArrRemark == "F" && chuyenBay.DepRemark == "F")
                        {
                            empCount[3] = empCount[3] - 1;
                        }
                    }

                }
                if (product.ShortName == "BXL" &&  product.ManQuota > 0)
                {
                    empCount[3] = empCount[3] + product.ManQuota;
                    int[] min = GetMinStartMinEndFLightExtensions.GetMinStartMinEnd(chuyenBay, product);
                    minStart[3] = min[0];
                    minEnd[3] = min[1];
                    if (XacDinhChuyenCargoDen(chuyenBay.FlightNo) || XacDinhChuyenCargoDi(chuyenBay.FlightNo))
                    {
                        empCount[3] = empCount[3] - 1;
                    }else{
                        if (chuyenBay.ArrRemark == "F" && chuyenBay.DepRemark == "F")
                        {
                            empCount[3] = empCount[3] - 1;
                        }
                    }
                }
                if (product.ShortName == "BXE")
                {
                    empCount[3] = empCount[3] + product.ManQuota;
                    int[] min = GetMinStartMinEndFLightExtensions.GetMinStartMinEnd(chuyenBay, product);
                    minStart[3] = min[0];
                    minEnd[3] = min[1];
                }
            }
            for (int i = 0 ; i < 4 ; i ++)
            {
                int indexStart = minStart[i];
                int indexEnd = minEnd[i];
                int slNhanVienTang = empCount[i];

                for ( int nextIndex= indexStart ; nextIndex <= indexEnd ; nextIndex ++ ){
                    if (i == 0)
                    {
                        trucNhanSuTheoNgay[nextIndex].NhanSuDke =
                            trucNhanSuTheoNgay[nextIndex].NhanSuDke + slNhanVienTang;
                    }
                    if (i == 1)
                    {
                        trucNhanSuTheoNgay[nextIndex].NhanSuXn =
                            trucNhanSuTheoNgay[nextIndex].NhanSuXn + slNhanVienTang;
                    }
                    if (i == 2)
                    {
                        trucNhanSuTheoNgay[nextIndex].NhanSuBta =
                            trucNhanSuTheoNgay[nextIndex].NhanSuBta + slNhanVienTang;
                    }
                    if (i == 3)
                    {
                        trucNhanSuTheoNgay[nextIndex].NhanSuBx =
                            trucNhanSuTheoNgay[nextIndex].NhanSuBx + slNhanVienTang;
                    }
                }

            }
            return trucNhanSuTheoNgay;
        }

        public List<TrucNhanSuTheoNgay> GiamNhanSuTaiTrucThoiGian(List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay)
        {
            for ( int i= 0 ; i < trucNhanSuTheoNgay.Count ; i ++ ){
                //if trucNhanSuTheoNgay[i].NhanSuDke <= 20 {
                //	trucNhanSuTheoNgay[i].NhanSuDke = int(math.Round(float64(trucNhanSuTheoNgay[i].NhanSuDke) - float64(trucNhanSuTheoNgay[i].NhanSuDke)*float64(5)/float64(100)))
                //}
                //if trucNhanSuTheoNgay[i].NhanSuDke > 20 {
                //	trucNhanSuTheoNgay[i].NhanSuDke = int(math.Round(float64(trucNhanSuTheoNgay[i].NhanSuDke) - float64(trucNhanSuTheoNgay[i].NhanSuDke)*float64(10)/float64(100)))
                //}
                //trucNhanSuTheoNgay[i].NhanSuXn = int(math.Round(float64(trucNhanSuTheoNgay[i].NhanSuXn) - float64(trucNhanSuTheoNgay[i].NhanSuXn) * float64(5) / float64 (100)))
                //trucNhanSuTheoNgay[i].NhanSuBta = int(math.Round(float64(trucNhanSuTheoNgay[i].NhanSuBta) - float64(trucNhanSuTheoNgay[i].NhanSuBta) * float64(10) / float64 (100)))
                if (trucNhanSuTheoNgay[i].NhanSuBx > 25 ){
                    trucNhanSuTheoNgay[i].NhanSuBx = (int)((float)trucNhanSuTheoNgay[i].NhanSuBx - (float)trucNhanSuTheoNgay[i].NhanSuBx * (float)25/(float)100);
                    continue;
                }
                if (trucNhanSuTheoNgay[i].NhanSuBx > 20) {
                    trucNhanSuTheoNgay[i].NhanSuBx = (int)((float)trucNhanSuTheoNgay[i].NhanSuBx - (float)trucNhanSuTheoNgay[i].NhanSuBx * (float)10/(float)100);
                    continue;
                }
                if (trucNhanSuTheoNgay[i].NhanSuBx > 15) {
                    trucNhanSuTheoNgay[i].NhanSuBx = (int)((float)trucNhanSuTheoNgay[i].NhanSuBx - (float)trucNhanSuTheoNgay[i].NhanSuBx * (float)5/(float)100);
                }
            }
            return trucNhanSuTheoNgay;
        }

        public List<TrucNhanSuTheoNgay> TinhNhanSuTheoGio(List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay)
        {
            List<TrucNhanSuTheoNgay> trucNhanSuTheoNgayNew = new List<TrucNhanSuTheoNgay>();
            for (int i = 0; i < danhSachMuiGioTrongNgay.Count; i++)
            {
                List<TrucNhanSuTheoNgay> trucNhanSuTheoGio = new List<TrucNhanSuTheoNgay>();
                for (int j = 0; j < trucNhanSuTheoNgay.Count ; j++){
                    if (trucNhanSuTheoNgay[j].Phut >= danhSachMuiGioTrongNgay[i].MinStart && trucNhanSuTheoNgay[j].Phut <= danhSachMuiGioTrongNgay[i].MinEnd){
                        trucNhanSuTheoGio.Add(trucNhanSuTheoNgay[j]);
                    }
                }
                // LAY NHAN SU MAX TRONG GIO
                TrucNhanSuTheoNgay trucNhanSuMaxTrongGio =
                    LaySoLuongNhanSuMax(trucNhanSuTheoGio, danhSachMuiGioTrongNgay[i]);
            }
            return trucNhanSuTheoNgayNew;
        }

        public TrucNhanSuTheoNgay LaySoLuongNhanSuMax(List<TrucNhanSuTheoNgay> trucNhanSu, MuiGio muiGio)
        {
            // LAY NHAN SU DKE MAX
            int maxDke = trucNhanSu.Max(phut => phut.NhanSuDke);
            // LAY NHAN SU XN MAX
            int maxXn = trucNhanSu.Max(phut => phut.NhanSuXn);
            // LAY NHAN SU BTA MAX
            int maxBta = trucNhanSu.Max(phut => phut.NhanSuBta);
            // LAY NHAN SU BX MAX
            int maxBx = trucNhanSu.Max(phut => phut.NhanSuBx);
            TrucNhanSuTheoNgay newTrucNhanSu = new TrucNhanSuTheoNgay();
            newTrucNhanSu.Phut = muiGio.MinStart;
            newTrucNhanSu.NhanSuDke = maxDke;
            newTrucNhanSu.NhanSuXn = maxXn;
            newTrucNhanSu.NhanSuBta = maxBta;
            newTrucNhanSu.NhanSuBx = maxBx;
            return newTrucNhanSu;
        }

        public List<NhanSuCuaKip> LayNhanSuCaBuoiSang(List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay)
        {
            List<NhanSuCuaKip> nhanSuCuaKipBuoiSang = new List<NhanSuCuaKip>();
            
            // LAY NHAN SU LAM CA LUC 4H DEN 16h
            NhanSuCuaKip nhanSuCuaKip4H_16H = KhoiTaoCaLamViec(240, 284, 960, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(240, 960, nhanSuCuaKip4H_16H,trucNhanSuTheoNgay);
            
            // LAY NHAN SU LAM CA LUC  4H45 DEN 16h
            NhanSuCuaKip nhanSuCuaKip4H45_16H = KhoiTaoCaLamViec(285,359, 960, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(285,359, nhanSuCuaKip4H45_16H,trucNhanSuTheoNgay);
            
            // LAY NHAN SU LAM CA LUC 6H00 DEN 16h
            NhanSuCuaKip nhanSuCuaKip6H_16H = KhoiTaoCaLamViec(360,419, 960, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(360,419, nhanSuCuaKip6H_16H,trucNhanSuTheoNgay);
            
            // LAY NHAN SU LAM CA LUC 7H DEN 17H
            NhanSuCuaKip nhanSuCuaKip7H_17H = KhoiTaoCaLamViec(420,539, 1020, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(420,539, nhanSuCuaKip7H_17H,trucNhanSuTheoNgay);
            
            // LAY NHAN SU LAM CA LUC 9H DEN 16H(19H)
            NhanSuCuaKip nhanSuCuaKip9H_19H = KhoiTaoCaLamViec(540,929, 1140, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(540,929, nhanSuCuaKip9H_19H,trucNhanSuTheoNgay);
            
            nhanSuCuaKipBuoiSang.Add(nhanSuCuaKip4H_16H);
            nhanSuCuaKipBuoiSang.Add(nhanSuCuaKip4H45_16H);
            nhanSuCuaKipBuoiSang.Add(nhanSuCuaKip6H_16H);
            nhanSuCuaKipBuoiSang.Add(nhanSuCuaKip7H_17H);
            nhanSuCuaKipBuoiSang.Add(nhanSuCuaKip9H_19H);
            return nhanSuCuaKipBuoiSang;
        }

        public List<NhanSuCuaKip> LayNhanSuCaChieu(List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay, List<NhanSuCuaKip> nhanSuCaBuoiSang)
        {
            List<NhanSuCuaKip> nhanSuCuaKipBuoiChieu = new List<NhanSuCuaKip>();
            
            // LAY NHAN SU LAM CA LUC 16H - 24H
            NhanSuCuaKip nhanSuCuaKip16H_24H = KhoiTaoCaLamViec(960,1019,1440, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(960,1019, nhanSuCuaKip16H_24H,trucNhanSuTheoNgay);
            
            // LAY NHAN SU LAM CA LUC 17H - 24H
            NhanSuCuaKip nhanSuCuaKip17H_24H = KhoiTaoCaLamViec(1020,1139,1440, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(1020,1139,nhanSuCuaKip17H_24H,trucNhanSuTheoNgay);
            
            // LAY NHAN SU LAM CA LUC 19H - 5H+
            NhanSuCuaKip nhanSuCuaKip19H_24H = KhoiTaoCaLamViec(1140,1439,1440, trucNhanSuTheoNgay);
            trucNhanSuTheoNgay = KhoiTaoLaiTrucNhanSuTheoNgay(1140,1439, nhanSuCuaKip19H_24H,trucNhanSuTheoNgay);
            
            nhanSuCuaKipBuoiChieu.Add(nhanSuCuaKip16H_24H);
            nhanSuCuaKipBuoiChieu.Add(nhanSuCuaKip17H_24H);
            nhanSuCuaKipBuoiChieu.Add(nhanSuCuaKip19H_24H);
            nhanSuCuaKipBuoiChieu = LayNhanSuCaDem(trucNhanSuTheoNgay, nhanSuCaBuoiSang, nhanSuCuaKipBuoiChieu);
            return nhanSuCuaKipBuoiChieu;   
        }

        public List<NhanSuCuaKip> LayNhanSuCaDem(List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay, List<NhanSuCuaKip> nhanSuCaBuoiSang, List<NhanSuCuaKip> nhanSuCaBuoiChieu )
        {
            NhanSuCuaKip nhanSuCuaKip6H_16H = nhanSuCaBuoiSang[2];
            
            // NHAN SU MAX TU 00h-3h30 sáng
            List<TrucNhanSuTheoNgay> trucNhanSuCaLamViecDem3H30 = new List<TrucNhanSuTheoNgay>();
            for (int i = 0; i < trucNhanSuTheoNgay.Count ; i++){
                if (trucNhanSuTheoNgay[i].Phut >= 1440 &&trucNhanSuTheoNgay[i].Phut <= 1650){
                    trucNhanSuCaLamViecDem3H30.Add(trucNhanSuTheoNgay[i]);
                }
            }
            TrucNhanSuTheoNgay thongTinNhanSuMaxTrongCaDem3H30 = LaySoLuongNhanSuMax(trucNhanSuCaLamViecDem3H30, new MuiGio(0, 0));
            
            // NHAN SU MAX TU 3H30 - 5H
            List<TrucNhanSuTheoNgay> trucNhanSuCaLamViecDem5H = new List<TrucNhanSuTheoNgay>();
            for (int i = 0; i < trucNhanSuTheoNgay.Count ; i++){
                if (trucNhanSuTheoNgay[i].Phut >= 1651 &&trucNhanSuTheoNgay[i].Phut <= 1740){
                    trucNhanSuCaLamViecDem5H.Add(trucNhanSuTheoNgay[i]);
                }
            }
            TrucNhanSuTheoNgay thongTinNhanSuMaxTrongCaDem5H = LaySoLuongNhanSuMax(trucNhanSuCaLamViecDem3H30, new MuiGio(0, 0));

          
            // SO SANH MAX XN CA 5H VS 3H30
            if (thongTinNhanSuMaxTrongCaDem5H.NhanSuXn >= thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn)
            {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn = 0;
            } else {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn = thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn - thongTinNhanSuMaxTrongCaDem5H.NhanSuXn;
            }
            // SO SANH MAX XN CA 5H VS 3H30
            if (thongTinNhanSuMaxTrongCaDem5H.NhanSuXn >= thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn)
            {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn = 0;
            } else
            {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn = thongTinNhanSuMaxTrongCaDem3H30.NhanSuXn -
                                                           thongTinNhanSuMaxTrongCaDem5H.NhanSuXn;
            }
            // SO SANH MAX BTA CA 5H VS 3H30
            if (thongTinNhanSuMaxTrongCaDem5H.NhanSuBta >= thongTinNhanSuMaxTrongCaDem3H30.NhanSuBta)
            {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuBta = 0;
            } else
            {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuBta = thongTinNhanSuMaxTrongCaDem3H30.NhanSuBta - thongTinNhanSuMaxTrongCaDem5H.NhanSuBta;
            }
            // SO SANH MAX BX CA 5H VS 3H30
            if (thongTinNhanSuMaxTrongCaDem5H.NhanSuBx >= thongTinNhanSuMaxTrongCaDem3H30.NhanSuBx)
            {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuBx = 0;
            } else
            {
                thongTinNhanSuMaxTrongCaDem3H30.NhanSuBx = thongTinNhanSuMaxTrongCaDem3H30.NhanSuBx -
                                                           thongTinNhanSuMaxTrongCaDem5H.NhanSuBx;
            }
            // NEU NHAN SU LUC 17H + 19H < NHAN SU MAX CA DEM 5H => CHUYEN NHAN SU CA SANG 6H LAM DEN 17H, 16H -> 17H
            int nhanSuDke17H19H = nhanSuCaBuoiChieu[1].ThoiGianLamViecCuaNhanSuDke.Count + nhanSuCaBuoiChieu[2].ThoiGianLamViecCuaNhanSuDke.Count;
            int nhanSuXN17H19H = nhanSuCaBuoiChieu[1].ThoiGianLamViecCuaNhanSuXn.Count + nhanSuCaBuoiChieu[2].ThoiGianLamViecCuaNhanSuXn.Count;
            int nhanSuBTA17H19H = nhanSuCaBuoiChieu[1].ThoiGianLamViecCuaNhanSuBta.Count + nhanSuCaBuoiChieu[2].ThoiGianLamViecCuaNhanSuBta.Count;
            int nhanSuBX17H19H = nhanSuCaBuoiChieu[1].ThoiGianLamViecCuaNhanSuBx.Count + nhanSuCaBuoiChieu[2].ThoiGianLamViecCuaNhanSuBx.Count;
            // DKE
	        if (thongTinNhanSuMaxTrongCaDem5H.NhanSuDke > nhanSuDke17H19H && nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuDke.Count > 0  &&  nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuDke.Count > 0 ){
		        for (int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuDke - nhanSuDke17H19H ; i ++ ){
			        if (nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuDke.Count == i)
                    {
                        break;
                    }
                    nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuDke[i].ThoiGianKetThuc = 1020;
                }
		        for ( int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuDke - nhanSuDke17H19H ; i ++) {
			        if (nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuDke.Count == i)
                    {
                        break;
                    }
                    nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuDke[i].ThoiGianBatDau = 1020;
                }
	        }
	        // XN
	        if (thongTinNhanSuMaxTrongCaDem5H.NhanSuXn > nhanSuXN17H19H && nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuXn.Count > 0 &&  nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuXn.Count > 0) {
		        for ( int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuXn - nhanSuXN17H19H ; i ++) {
			        if (nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuXn.Count == i){
                        break;
                    }
                    nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuXn[i].ThoiGianKetThuc = 1020;
                }
		        for (int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuXn - nhanSuXN17H19H ; i ++ ){
			        if (nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuXn.Count == i)
                    {
                        break;
                    }
                    nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuXn[i].ThoiGianBatDau = 1020;
                }
	        }
	        // BTA
	        if (thongTinNhanSuMaxTrongCaDem5H.NhanSuBta > nhanSuBTA17H19H && nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuBta.Count > 0 &&  nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuBta.Count > 0) {
		        for (int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuBta - nhanSuBTA17H19H ; i ++ ){
			        if (nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuBta.Count == i)
                    {
                        break;
                    }
                    nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuBta[i].ThoiGianKetThuc = 1020;
                }
		        for (int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuBta - nhanSuBTA17H19H ; i ++ ){
			        if (nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuBta.Count == i)
                    {
                        break;
                    }
                    nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuBta[i].ThoiGianBatDau = 1020;
                }
	        }
	        // BX
	        if (thongTinNhanSuMaxTrongCaDem5H.NhanSuBx > nhanSuBX17H19H && nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuBx.Count > 0 && nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuBx.Count > 0){
		        for (int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuBx - nhanSuBX17H19H ; i ++ ){
			        if (nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuBx.Count == i)
                    {
                        break;
                    }
                    nhanSuCuaKip6H_16H.ThoiGianLamViecCuaNhanSuBx[i].ThoiGianKetThuc = 1020;
                }

		        for (int i = 0 ; i < thongTinNhanSuMaxTrongCaDem5H.NhanSuBx - nhanSuBX17H19H ; i ++ ){
			        if (nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuBx.Count == i)
                    {
                        break;
                    }
                    nhanSuCaBuoiChieu[0].ThoiGianLamViecCuaNhanSuBx[i].ThoiGianBatDau = 1020;
                }
	        }

            nhanSuCaBuoiChieu = CaiDatThoiGianKetThucCaLamDem5h(thongTinNhanSuMaxTrongCaDem5H,
                thongTinNhanSuMaxTrongCaDem3H30, nhanSuCaBuoiChieu);
            nhanSuCaBuoiSang[2] = nhanSuCuaKip6H_16H;
            return nhanSuCaBuoiChieu;
        }

        public List<NhanSuCuaKip> CaiDatThoiGianKetThucCaLamDem5h(TrucNhanSuTheoNgay nhanSuMaxYc5H, TrucNhanSuTheoNgay nhanSuMaxYc3H30 ,List<NhanSuCuaKip> nhanSuCuaKipBuoiToi)
        {
            // DKE
            nhanSuCuaKipBuoiToi = CaiDatDKE(nhanSuMaxYc5H.NhanSuDke, nhanSuCuaKipBuoiToi, 5);
            // XN
            nhanSuCuaKipBuoiToi = CaiDatXN(nhanSuMaxYc5H.NhanSuXn, nhanSuCuaKipBuoiToi, 5);
            // BTA
            nhanSuCuaKipBuoiToi = CaiDatBTA(nhanSuMaxYc5H.NhanSuBta, nhanSuCuaKipBuoiToi, 5);
            // BX
            nhanSuCuaKipBuoiToi = CaiDatBX(nhanSuMaxYc5H.NhanSuBx, nhanSuCuaKipBuoiToi, 5);

            //-------

            // DKE
            nhanSuCuaKipBuoiToi = CaiDatDKE(nhanSuMaxYc3H30.NhanSuDke, nhanSuCuaKipBuoiToi, 3);
            // XN
            nhanSuCuaKipBuoiToi = CaiDatXN(nhanSuMaxYc3H30.NhanSuXn, nhanSuCuaKipBuoiToi, 3);
            // BTA
            nhanSuCuaKipBuoiToi = CaiDatBTA(nhanSuMaxYc3H30.NhanSuBta, nhanSuCuaKipBuoiToi, 3);
            // BX
            nhanSuCuaKipBuoiToi = CaiDatBX(nhanSuMaxYc3H30.NhanSuBx, nhanSuCuaKipBuoiToi, 3);

            return nhanSuCuaKipBuoiToi;
        }
        
        public List<NhanSuCuaKip> CaiDatDKE(int nhanSuMaxYc5H , List<NhanSuCuaKip> nhanSuCuaKipBuoiToi ,int thamSoXacDinhGio)
{
    int countDke = 0;
	for (int i = 2 ; i >= 0 ; i--){
		for ( int j = 0 ; j < nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuDke.Count; j ++){
			if (countDke == nhanSuMaxYc5H)
            {
                break;
            }
            if (nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuDke[j].ThoiGianKetThuc > 1440)
            {
                continue;
            }
			if(thamSoXacDinhGio == 5 )
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuDke[j].ThoiGianKetThuc = 1740;
            }else
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuDke[j].ThoiGianKetThuc = 1650;
            }

            countDke++;
        }
		if (countDke == nhanSuMaxYc5H)
        {
            break;
        }
	}

    return nhanSuCuaKipBuoiToi;
}

        public List<NhanSuCuaKip> CaiDatXN(int nhanSuMaxYc5H , List<NhanSuCuaKip> nhanSuCuaKipBuoiToi ,int thamSoXacDinhGio)
{
    int countXN = 0;
	for (int i = 2 ; i >= 0 ;  i--){
		for (int j = 0; j < nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuXn.Count; j ++){
			if (countXN == nhanSuMaxYc5H)
            {
                break;
            }
			if (nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuXn[j].ThoiGianKetThuc > 1440)
            {
                continue;
            }
			if (thamSoXacDinhGio == 5 )
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuXn[j].ThoiGianKetThuc = 1740;
            }else
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuXn[j].ThoiGianKetThuc = 1650;
            }
            countXN++;
        }
		if (countXN == nhanSuMaxYc5H)
        {
            break;
        }
	}

    return nhanSuCuaKipBuoiToi;
}

        public List<NhanSuCuaKip> CaiDatBTA(int nhanSuMaxYc5H , List<NhanSuCuaKip> nhanSuCuaKipBuoiToi ,int thamSoXacDinhGio)
{
    int countBta = 0;
	for (int i = 2 ; i >= 0 ;  i--){
		for (int j = 0 ; j < nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBta.Count ; j++){
			if (countBta == nhanSuMaxYc5H)
            {
                break;
            }
			if (nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBta[j].ThoiGianKetThuc > 1440)
            {
                continue;
            }
			if (thamSoXacDinhGio == 5 )
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBta[j].ThoiGianKetThuc = 1740;
            }else
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBta[j].ThoiGianKetThuc = 1650;
            }
            countBta++;
        }
		if (countBta == nhanSuMaxYc5H)
        {
            break;
        }
	}
    return nhanSuCuaKipBuoiToi;
}

        public List<NhanSuCuaKip> CaiDatBX(int nhanSuMaxYc5H , List<NhanSuCuaKip> nhanSuCuaKipBuoiToi ,int thamSoXacDinhGio)
{
    int countBx = 0;
	for (int i = 2 ; i >= 0 ;  i--){
		for (int j = 0 ; j <  nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBx.Count; j ++){
			if (countBx == nhanSuMaxYc5H)
            {
                break;
            }
			if (nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBx[j].ThoiGianKetThuc > 1440)
            {
                continue;
            }
			if (thamSoXacDinhGio == 5 )
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBx[j].ThoiGianKetThuc = 1740;
            }else
            {
                nhanSuCuaKipBuoiToi[i].ThoiGianLamViecCuaNhanSuBx[j].ThoiGianKetThuc = 1650;
            }

            countBx++;
        }
		if (countBx == nhanSuMaxYc5H)
        {
            break;
        }
	}

    return nhanSuCuaKipBuoiToi;
}

        public NhanSuCuaKip KhoiTaoCaLamViec(int thoiGianBatDau, int thoiGianNgatCa, int thoiGianKetThuc, List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay)
        {
            List<TrucNhanSuTheoNgay> trucNhanSuCaLamViec = new List<TrucNhanSuTheoNgay>();
            
            foreach (TrucNhanSuTheoNgay realTime in trucNhanSuTheoNgay) {
                if (realTime.Phut >= thoiGianBatDau && realTime.Phut <= thoiGianNgatCa){
                    trucNhanSuCaLamViec.Add(realTime);
                } 
            }
            // LAY SO LUONG NHAN SU MAX CUA CA TRUYEN VAO
            TrucNhanSuTheoNgay thongTinNhanSuMaxTrongCa = LaySoLuongNhanSuMax(trucNhanSuCaLamViec, new MuiGio(0, 0));
            
            // TAO NHAN SU DKE
            List<ThoiGianLamViecCuaNhanSu> danhSachNhanSuDke = new List<ThoiGianLamViecCuaNhanSu>();
            for ( int i = 0; i < thongTinNhanSuMaxTrongCa.NhanSuDke ; i ++){
                danhSachNhanSuDke.Add(new ThoiGianLamViecCuaNhanSu(i+1,thoiGianBatDau,thoiGianKetThuc));
            }
            
            // TAO NHAN SU XN
            List<ThoiGianLamViecCuaNhanSu> danhSachNhanSuXn = new List<ThoiGianLamViecCuaNhanSu>();
            for ( int i = 0; i < thongTinNhanSuMaxTrongCa.NhanSuXn ; i ++){
                danhSachNhanSuXn.Add(new ThoiGianLamViecCuaNhanSu(i+1,thoiGianBatDau,thoiGianKetThuc));
            }
            
            // TAO NHAN SU BTA
            List<ThoiGianLamViecCuaNhanSu> danhSachNhanSuBta = new List<ThoiGianLamViecCuaNhanSu>();
            for ( int i = 0; i < thongTinNhanSuMaxTrongCa.NhanSuBta ; i ++){
                danhSachNhanSuBta.Add(new ThoiGianLamViecCuaNhanSu(i+1,thoiGianBatDau,thoiGianKetThuc));
            }
            
            // TAO NHAN SU BX
            List<ThoiGianLamViecCuaNhanSu> danhSachNhanSuBx = new List<ThoiGianLamViecCuaNhanSu>();
            for ( int i = 0; i < thongTinNhanSuMaxTrongCa.NhanSuBx ; i ++){
                danhSachNhanSuBx.Add(new ThoiGianLamViecCuaNhanSu(i+1,thoiGianBatDau,thoiGianKetThuc));
            }
            
            // GOP THANH DANH SACH NHAN SU CUA KIP
            NhanSuCuaKip nhanSuCuaKip = new NhanSuCuaKip(1, thoiGianBatDau, danhSachNhanSuDke, danhSachNhanSuXn,
                danhSachNhanSuBta, danhSachNhanSuBx);

            return nhanSuCuaKip;
        }

        public List<TrucNhanSuTheoNgay> KhoiTaoLaiTrucNhanSuTheoNgay(int thoiGianBatDau, int thoiGianKetThuc, NhanSuCuaKip nhanSuCuaKip, List<TrucNhanSuTheoNgay> trucNhanSuTheoNgay)
        {
            for (int i = 0; i < trucNhanSuTheoNgay.Count; i++)
            {
                if (trucNhanSuTheoNgay[i].Phut >= thoiGianBatDau && trucNhanSuTheoNgay[i].Phut <= thoiGianKetThuc){
                    // GIAM SO LUONG DKE
                    if (trucNhanSuTheoNgay[i].NhanSuDke > 0)
                    {
                        trucNhanSuTheoNgay[i].NhanSuDke = trucNhanSuTheoNgay[i].NhanSuDke -
                                                          nhanSuCuaKip.ThoiGianLamViecCuaNhanSuDke.Count;
                    }
                    // GIAM SO LUONG XN
                    if (trucNhanSuTheoNgay[i].NhanSuXn > 0)
                    {
                        trucNhanSuTheoNgay[i].NhanSuXn = trucNhanSuTheoNgay[i].NhanSuXn -
                                                         nhanSuCuaKip.ThoiGianLamViecCuaNhanSuXn.Count;
                    }
                    // GIAM SO LUONG BTA
                    if (trucNhanSuTheoNgay[i].NhanSuBta > 0)
                    {
                        trucNhanSuTheoNgay[i].NhanSuBta = trucNhanSuTheoNgay[i].NhanSuBta -
                                                          nhanSuCuaKip.ThoiGianLamViecCuaNhanSuBta.Count;
                    }
                    // GIAM SO LUONG BX
                    if (trucNhanSuTheoNgay[i].NhanSuBx > 0)
                    {
                        trucNhanSuTheoNgay[i].NhanSuBx = trucNhanSuTheoNgay[i].NhanSuBx -
                                                         nhanSuCuaKip.ThoiGianLamViecCuaNhanSuBx.Count;
                    }
                }
            }

            return trucNhanSuTheoNgay;
        }
    }
}