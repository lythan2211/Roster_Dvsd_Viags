using Roster.Business.Models;

namespace Roster
{
   public class GlobalData
    {
        private static GlobalData _instance = new GlobalData();

        public static GlobalData Instance
        {
            get { return _instance; }
        }

        public UserSessionInfo currentUser { get; set; }
    }
}
