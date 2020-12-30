using Domain.Common;
using Domain.Dispather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Twitter
{
    public class TwitterReportStoreRequestHandler : ICommandHandler
    {
        public async Task Handle(ICommand Command)
        {
            var Commands = Command as TwitterReportRequest;
            switch (Commands.CommandType)
            {
                case Common.Common.CommandType.Save:
                    await SaveReport(Commands);
                    break;
                case Common.Common.CommandType.ReadReport:
                    await ReadReportAsync(Commands);
                    break;
                case Common.Common.CommandType.ReadData:
                     await ReadStore(Commands);
                    break;
                case Common.Common.CommandType.Other:
                    Commands.ProcessStatus = new Response()
                    {
                        Status = Common.Common.ProcessState.Fail,
                        StatusMessage = $"Other type of Command is not accepted"
                    };
                    break;
                default:
                    Commands.ProcessStatus = new Response()
                    {
                        Status = Common.Common.ProcessState.Fail,
                        StatusMessage = $"Command type is not accepted"
                    };
                    break;
            }

        }
        private async Task ReadReportAsync(TwitterReportRequest Data)
        {
            try
            {
                string FolderPath = ""; ;
                var ReportFiles = new List<string>();
                if (Data.Display)
                {
                    FolderPath = $"{AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("Statitics"))}" +
                                                  $"BatchJobs//{Data.ReportFilePath}";
                    ReportFiles = Directory.EnumerateFiles(FolderPath).Select(file => Path.GetFileName(file)).ToList();
                }
                else
                {
                    FolderPath = $"{AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"))}" +
                                                $"{Data.ReportFilePath}";
                    ReportFiles = Directory.EnumerateFiles(FolderPath).Select(file => Path.GetFileName(file)).ToList();
                }
                if (ReportFiles != null && ReportFiles.Count() > 0)
                {
                    Data.Report = await Task.Run(() =>
                    JsonConvert.DeserializeObject<TwitterReport>(File.ReadAllText($"{FolderPath}{ReportFiles.ToList()[0]}", Encoding.Unicode)

                    ));
                    
                }
                else
                {
                    Data.Report = new TwitterReport();
                }

                

            }
            catch (Exception ex)
            {
                Data.ProcessStatus = new Common.Response()
                {
                    Status = Common.Common.ProcessState.Fail,
                    StatusMessage = $"Error : (TwitterStoreHandler) {ex.Message}"
                };
            }

        }
        private async Task ReadStore(TwitterReportRequest Data)
        {
            try
            {
               
                var path = $"{AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"))}{Data.StoreFilePath}";
                var SourceData = new DirectoryInfo(path).GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .Reverse()
                         .ToList();
                if (SourceData != null && SourceData.Count() > 0)
                {
                    Data.SourceData = new Dictionary<string, List<string>>();
                    //await Task.Run(() =>
                    //{
                    foreach (FileInfo file in SourceData)
                    {
                        
                        var content = File.ReadAllLines( file.FullName, Encoding.Unicode).ToList<string>();
                        Data.SourceData.Add(file.Name, content);
                        content = null;
                        file.MoveTo($"{file.DirectoryName}\\Archive\\{file.Name}");
                    }
                    //});
                }
                
            }
            catch (Exception ex)
            {
                Data.ProcessStatus = new Common.Response()
                {
                    Status = Common.Common.ProcessState.Fail,
                    StatusMessage = $"Error : (TwitterStoreHandler) {ex.Message}"
                };
            }

        }

        private async Task SaveReport(TwitterReportRequest Data)
        {
            try
            {
                var Directory = $"{AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"))}{Data.ReportFilePath}";
                var FileName = $"{Data.ReportFileName}{Guid.NewGuid()}{Data.FileExtension}";
                using (var fs = new FileStream(
                                                $"{Directory}{FileName}",
                                                FileMode.OpenOrCreate,
                                                FileAccess.ReadWrite,
                                                FileShare.Read,
                                                4096,
                                                true
                                                ))
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(Data.Report).ToString());
                    await fs.WriteAsync(bytes, 0, bytes.Length);
                }
                var files = new DirectoryInfo(Directory).GetFiles();
                foreach (FileInfo file in files.Where(f => !f.Name.Contains(FileName)))
                {
                    file.Delete();
                }
                 
            }
            catch (Exception ex)
            {
                Data.ProcessStatus = new Common.Response()
                {
                    Status = Common.Common.ProcessState.Fail,
                    StatusMessage = $"Error : (TwitterStoreHandler) {ex.Message}"
                };
            }
        }
    }
}
