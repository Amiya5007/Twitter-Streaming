using Domain.Common;
using Domain.Dispather;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Twitter
{
    public class GetTwitterReportRequest: TwitterRequest
    {
       
        public TwitterReport Report { get; set; }
        
        public GetTwitterReportRequest()
        {
            Report = new TwitterReport();
        }

    }
}
