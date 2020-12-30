using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatchJobs
{
    public interface IClient
    {
        Task Handle();
    }
}
