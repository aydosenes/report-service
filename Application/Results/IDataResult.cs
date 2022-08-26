using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Results
{
    public interface IDataResult<T> : IBaseResult
    {
        T Data { get; }
    }
}
