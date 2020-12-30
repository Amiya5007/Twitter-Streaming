using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 
using Domain.Twitter;
using Domain.Dispather;
using System.Diagnostics;

namespace Domain.Twitter
{
    public class TwitterHandler :  ICommandHandler
    {
        private readonly StringBuilder StreamData = new StringBuilder();
        int count = 0;
        int fileCount = 0;
        
        public async Task Handle(ICommand Command)
        {
            var Commands = Command as TwitterRequest;
            switch (Commands.CommandType)
            {
                case Common.Common.CommandType.GetReport:
                    await GetReport(Command);
                    break;
                default:
                   await StreamApi(Command);
                    break;
            } 
        }
        private async Task GetReport(ICommand Command)
        {
            var Commands = Command as GetTwitterReportRequest;
            TwitterReportRequest GetReportRequest = new TwitterReportRequest(Command)
            {
                Display = true
            };
            var Dispatcher = new Dispatcher();
            await Dispatcher.Send(GetReportRequest);
            Commands.Report = GetReportRequest.Report;
            
        }
        private async Task StreamApi(ICommand Command)
        {
            var Commands = Command as TwitterRequest;
            var StoreRequest = new TwitterSroreRequest(Command);
            // var ReportRequest = new TwitterReportRequest(Command);
            var Dispatcher = new Dispatcher();
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                var stream = new TwitterStreamClient();

                stream.TweetReceivedEvent += async (sender, args) =>
                {
                    if (args.Tweet != null && args.Tweet.Text != null)
                    {

                        count++;
                        StreamData.AppendLine($"{args.Tweet.Text.Trim().Replace('\n', ' ')}");
                        if (count % Commands.BatchVolume == 0)
                        {
                            stopwatch.Stop();
                            StoreRequest.Data = $"{stopwatch.ElapsedMilliseconds}\n {StreamData}";
                            StoreRequest.CommandType = Common.Common.CommandType.Save;
                            await Dispatcher.Send(StoreRequest);
                            fileCount++;
                            StreamData.Clear();
                            stopwatch.Restart();
                            if (fileCount % Commands.ReportProcessVolume == 0)
                            {
                                stopwatch.Stop();
                                await Dispatcher.Send(new TwitterReportRequest(Command));
                                stopwatch.Restart();
                            }
                        }
                    }
                };

                stream.ExceptionReceived += async (sender, exception) =>
                {
                    await Task.Run(() => Commands.ProcessStatus = new Common.Response()
                    {
                        Status = Common.Common.ProcessState.Fail,
                        StatusMessage = exception.TwitterException.ToString()

                    });
                };
                stopwatch.Start();
                await stream.Start(ApiKey: Commands.ApiKey, ApiUrl: Commands.ApiUrl);

            }
            catch (Exception ex)
            {
                Commands.ProcessStatus = new Common.Response()
                {
                    Status = Common.Common.ProcessState.Fail,
                    StatusMessage = $" Error:(TwitterHandler) {ex.Message}"
                };
            }
        }
    }
}

