using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class Dispatcher : IDispatcher<ICommand>
    {

        public async Task Send(ICommand Command)
        {
            var handler = Factory.GetCommand(Command.RequestType);
            await handler.Handle(Command);
             
        }
    }
}
