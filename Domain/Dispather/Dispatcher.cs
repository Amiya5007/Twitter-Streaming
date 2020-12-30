using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dispather
{
    public class Dispatcher : IDispatcher<ICommand>
    {
        public async Task Send(ICommand Command)
        {
            var handler = Factory.GetCommand(Command.RequestType);
            if (handler != null)
                await handler.Handle(Command);
            else
                Command.ProcessStatus = new Common.Response() { Status = Common.Common.ProcessState.Fail,
                                                                StatusMessage = "Dispatcher can not find the requested handler" };
        }
    }
}
