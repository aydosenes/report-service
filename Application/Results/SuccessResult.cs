using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Results
{
    public class SuccessResult : BaseResult
    {
        public SuccessResult(string message) : base(true, message)
        {

        }
        public SuccessResult() : base(true)
        {

        }
    }
}
