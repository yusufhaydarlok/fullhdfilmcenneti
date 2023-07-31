using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_core.Utilities
{
    public static class ErrorMessages
    {
        #region GENERAL
        public const string UNAUTHORIZED = "Unauthorized!";
        public const string DONT_HAVE_PERMISSION = "You do not have a permission!";
        public const string ACCESS_DENIED = "Access denied!";
        public const string CREDENTIALS_ARE_WRONG = "Credentials are wrong!";
        public const string SOMETHING_WENT_WRONG = "Something went wrong!";
        public const string INVALID_REQUEST = "Invalid request!";
        public const string SIGNATURES_ARE_NOT_MATCHED = "Signatures are not matched!";
        public const string APP_LOGIN_FAIL = "This user not allowed to login with app!";
        public const string MISSING_PARAMETER = "Missing Parameter!";
        public const string SERVICE_REQUEST = "Service Request";
        public const string SERVICE_REQUEST_ERROR = "Error";
        public const string REJECTED_ILOGGER_TYPE = "Please use a class which type is ILogger.";
        public const string REJECTED_IVALIDATOR_TYPE = "Please use a class which type is IValidator.";
        public const string NOT_FOUND = "Data not found!";
        #endregion

        public static string NotFound(string item)
        {
            return $"{item} not found!";
        }

        public static string GetMessage(string type, string action)
        {
            return $"{type} not {action}";
        }
    }
}
