using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Dispather
{
    public interface ICommand
    {
        Common.Common.BatchType RequestType { get; set;}
        Response ProcessStatus { get; set; }
    }
}
