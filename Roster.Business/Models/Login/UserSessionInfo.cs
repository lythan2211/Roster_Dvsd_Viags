using Roster.Data.Utils;

namespace Roster.Business.Models
{
    public class UserSessionInfo
    {

        #region Fields, Properties
        [DataConvert("UserSID")]
        public int Id { get; set; }
        [DataConvert("UserID")]
        public string UserId { get; set; }
        [DataConvert("UserName")]
        public string UserName { get; set; }
        [DataConvert("Email")]
        public string Email { get; set; }
        [DataConvert("Password")]
        public string Password { get; set; }
        [DataConvert("EmployeerId")]
        public int? EmployeerId { get; set; }
        [DataConvert("Location")]
        public string Location { get; set; }


        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserSessionInfo()
        {
        }

        public UserSessionInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, UserSessionInfo>(srcObject, this);
            }
        }

        #endregion
    }
}
