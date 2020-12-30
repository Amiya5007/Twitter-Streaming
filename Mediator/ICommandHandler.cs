﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
     
    public interface ICommandHandler 
    {
        Task Handle(ICommand Command);
    }
}
