using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Results
{
    public class BaseResult : IBaseResult
    {
        public BaseResult(bool success, string message) : this(success)
        {
            Message = message;
        }
        public BaseResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
