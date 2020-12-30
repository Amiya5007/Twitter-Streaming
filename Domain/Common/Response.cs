using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public class Response
    {
        public Common.ProcessState Status { get; set; }
        public string StatusMessage { get; set; }
        public Response()
        {
            Status = Common.ProcessState.Success;
            StatusMessage = "Success";
        }
    }
}
