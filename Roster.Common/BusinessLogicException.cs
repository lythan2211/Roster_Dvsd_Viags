﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster.Common
{
    public class BusinessLogicException : Exception
    {
        public ResultCode ErrorCode { get; private set; }

        public BusinessLogicException()
            : base()
        {
            this.ErrorCode = ResultCode.NoError;
        }

        public BusinessLogicException(ResultCode errorCode)
            : base()
        {
            this.ErrorCode = errorCode;
        }

        public BusinessLogicException(ResultCode errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public BusinessLogicException(ResultCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }
    }
}
