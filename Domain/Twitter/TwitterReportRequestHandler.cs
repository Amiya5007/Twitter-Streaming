using Domain.Common;
using Domain.Dispather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Twitter
{
    class TwitterReportRequestHandler : ICommandHandler
    {
        private readonly ReportUtility reportUtility = new ReportUtility();
        public async Task Handle(ICommand Command)
        {
            var Commands = Command as TwitterReportRequest;
            if (Commands.CommandType.Equals(Common.Common.CommandType.GetReport))
            {
                await GetCurrentReport(Commands);
            }
            else
            {
                await GetCurrentReport(Commands);
                await GetSourceData(Commands);
                await PrepareReport(Commands);
                await SaveReport(Commands);
            }

        }
        
        private async Task GetCurrentReport(TwitterReportRequest Commands)
        {
            if (Commands.ProcessStatus.Status.Equals(Common.Common.ProcessState.Success))
            {
                Commands.CommandType = Common.Common.CommandType.ReadReport;
                Commands.RequestType = Common.Common.BatchType.TwitterReportStore;
                var dispatcher = new Dispatcher();
                await dispatcher.Send(Commands);
            }
        }
        private async Task GetSourceData(TwitterReportRequest Commands)
        {
            if (Commands.ProcessStatus.Status.Equals(Common.Common.ProcessState.Success))
            {
                Commands.CommandType = Common.Common.CommandType.ReadData;
                Commands.RequestType = Common.Common.BatchType.TwitterReportStore;
                var dispatcher = new Dispatcher();
                await dispatcher.Send(Commands);
            }
        }

        private async Task PrepareReport(TwitterReportRequest Commands)
        {
            if (Commands.ProcessStatus.Status.Equals(Common.Common.ProcessState.Success))
            {
                await Task.Run(() => { reportUtility.GetAllReports(Commands); });
            }
        }
        private async Task SaveReport(TwitterReportRequest Commands)
        {
            if (Commands.ProcessStatus.Status.Equals(Common.Common.ProcessState.Success))
            {
                Commands.CommandType = Common.Common.CommandType.Save;
                Commands.RequestType = Common.Common.BatchType.TwitterReportStore;
                var dispatcher = new Dispatcher();
                await dispatcher.Send(Commands);
            }
        }
    }
}
