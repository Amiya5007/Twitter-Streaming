using Domain.Common;
using Domain.Dispather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Twitter
{
    class TwitterStoreRequestHandler : ICommandHandler
    {
        public async Task Handle(ICommand Command)
        {
            var Commands = Command as TwitterSroreRequest;
            switch (Commands.CommandType)
            {
                case Common.Common.CommandType.Save:
                    await SaveDataAsync(Commands);
                    break;
                case Common.Common.CommandType.ReadReport:
                    
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
        private async Task<TwitterSroreRequest> SaveDataAsync(TwitterSroreRequest Data)
        {
            try
            {
                using (var fs = new FileStream(
                                                $"{AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"))}" +
                                                $"{Data.StoreFilePath}" +
                                                $"{Data.StoreFileName}" +
                                                $"{Guid.NewGuid()}" +
                                                $"{Data.FileExtension}",
                                                FileMode.OpenOrCreate,
                                                FileAccess.ReadWrite,
                                                FileShare.Read,
                                                4096,
                                                true
                                                ))
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(Data.Data);
                    await fs.WriteAsync(bytes, 0, bytes.Length);
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
            return Data;
        }

      

    }
}
