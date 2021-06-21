using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster.Common
{
    public enum ResultCode : int
    {
        NoError = 1,
        UnknownError,
        TokenInvalid,
        NotFoundResourceId,
        IdNotMatch,
        NotModified,
        WaitNextRequest,
        DataInvalid = 8,
        DataIsUsed = 9,
        FileLarge = 10,
    }
}
