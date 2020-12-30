using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IStore<T> where T :IStoreRequest
    {
        Task Save(T data);
    }
}
