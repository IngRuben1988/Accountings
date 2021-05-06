using System;

namespace VTAworldpass.VTACore.Cores.Responses
{
    [Serializable]
    public class JResponses
    {
        private const string STATUS_OK          = "0";
        private const string STATUS_ERROR       = "1";
        private const string REGISTER_NOT_FOUND = "3";

        public static JModel ResultSuccess(object data, string message = null)
        {
            var apiResult     = new JModel();
            apiResult.status  = STATUS_OK;
            apiResult.message = message;
            apiResult.data    = data;
            apiResult.success = true;
            return apiResult;
        }

        public static JModel ResultError(string message, string stackTrace = null)
        {
            var apiResult        = new JModel();
            apiResult.status     = STATUS_ERROR;
            apiResult.message    = message;
            apiResult.stackTrace = stackTrace;
            apiResult.success    = false;
            return apiResult;
        }

        public static JModel ResultRegisterNotFound(string message)
        {
            var apiResult     = new JModel();
            apiResult.status  = REGISTER_NOT_FOUND;
            apiResult.message = message;
            apiResult.success = false;
            return apiResult;
        }


        public static string SuccessResultOK(object data, string message = null)
        {
            var apiResult     = new JModel();
            apiResult.status  = STATUS_OK;
            apiResult.message = message;
            apiResult.data    = data;
            apiResult.success = true;
            return apiResult.ToString();
        }

        public static string ErrorResultOK(string message)
        {
            var apiResult     = new JModel();
            apiResult.status  = STATUS_ERROR;
            apiResult.message = message;
            apiResult.success = false;
            return apiResult.ToString();
        }
    }
}