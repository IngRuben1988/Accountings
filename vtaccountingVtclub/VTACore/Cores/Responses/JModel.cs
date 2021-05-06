using System;

namespace VTAworldpass.VTACore.Cores.Responses
{
    [Serializable]
    public class JModel
    {
        public string status     { get; set; }
        public string message    { get; set; }
        public string stackTrace { get; set; }
        public object data       { get; set; }
        public bool   success    { get; set; }
    }
}