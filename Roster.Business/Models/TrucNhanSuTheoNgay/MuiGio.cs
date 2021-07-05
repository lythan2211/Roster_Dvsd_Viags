namespace Roster.Business.Models.TrucNhanSuTheoNgay
{
    public class MuiGio
    {
        public int MinStart { get; set; }
        public int MinEnd { get; set; }

        public MuiGio(int minStart, int minEnd)
        {
            MinStart = minStart;
            MinEnd = minEnd;
        }
    }
    
}