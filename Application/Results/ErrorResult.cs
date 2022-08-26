using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Results
{
    public class ErrorResult : BaseResult
    {
        public ErrorResult(string message) : base(false, message)
        {

        }
        public ErrorResult() : base(false)
        {

        }
    }
}
