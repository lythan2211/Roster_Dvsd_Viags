namespace Roster.Business.Models
{
    public class ProductDetail
    {
        public int Id                     { get; set; }    
        
        public string ProductCode         { get; set; } 
        
        public string ProductName         { get; set; } 
        
        public string ShortName           { get; set; } 
        
        public string FkAirline           { get; set; } 
        
        public string Ac                  { get; set; } 
        
        public string LoadProperty        { get; set; } 
        
        public string Carry               { get; set; } 
        
        public int ArrDep                 { get; set; } 
        
        public string DomInt              { get; set; } 
        
        public string TimeBase            { get; set; } 
        
        public int StartTime              { get; set; } 
        
        public int DuringTime             { get; set; } 
        
        public int ManQuota               { get; set; } 
        
        //public string    MinutesOnePerson float32
        public int   UnitPrice            { get; set; } 
        
        // employee list serve this product
        // public string    empList []*Employee
        
        // count of move Employee
        public int  moveCount    { get; set; } 
        
        // can move time
        public int  moveTime     { get; set; } 
        
        // is Employee moved
        public int  moveState    { get; set; } 
            
        // mark that this product get enough worker => can apply move to it
        public bool isFullServe  { get; set; } 
            
        // is all employee is added from other product
        public int  manReused    { get; set; } 
            
        // KHUNG GIO DAC BIET
        public string realTimeLine  { get; set; } 
    }
}