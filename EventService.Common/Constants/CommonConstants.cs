using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.Common.Constants
{
    public static class CommonConstants
    {

        public static readonly string STATUS_BUSY = "Busy";
        public static readonly string STATUS_OUT_OF_OFFICE = "OutOfOffice";
        public static readonly string RESP_PARAM_EMAIL = "email";
        public static readonly string RESP_PARAM_EVENTS = "events";
        public static readonly string RESP_PARAM_NO_OF_EVENTS = "number_of_events";
        public static readonly string RESP_PARAM_MESSAGE = "message";
        public static readonly string FETCH_SUCCESS = "Fetched items successfully";
        public static readonly string COMMON_SUCCESS = "Success";
        public static readonly string COMMON_FAILURE = "Failure";
        public static readonly string COMMON_EXCEPTION = "Exception";
        public static readonly string INV_EMAIL_ID = "The email id entered in the request is invalid.";

        //Logging Messages
        public static readonly string LOG_MSG_CRI_INV_EMAIL = "Request contained an invalid email id : {0)";
        public static readonly string LOG_MSG_INFO_STARTED = "GET Request initiated for the email id : {0}";
        public static readonly string LOG_MSG_ERR_FAILURE = "Failed to fetch response from internal API(s)";
    }
}
