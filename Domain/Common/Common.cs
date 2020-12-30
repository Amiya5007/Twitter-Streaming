using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public static class Common
    {
        public enum CommandType
        {
            Save,
            Delete,
            ReadData,
            ReadReport,
            GetReport,
            Other
        }
        public enum BatchType
        {
            Twitter,
            TwitterStore,
            TwitterReport,
            TwitterReportStore,
            Other,
        }
        public enum StreamState
        {
            Stopped = 0,
            Running = 1
        }
        public enum ProcessState
        {
            Fail,
            Success
        }
    }
}
