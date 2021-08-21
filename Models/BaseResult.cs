using System;
using System.Collections.Generic;

namespace Result
{
    public class BaseResult : IResult
    {
        public bool IsSuccess { get; set; } = false;
        public string SuccessMessage { get; set; } = string.Empty;
        public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ResultStatus Status => throw new NotImplementedException();

        public string SuccessMessage => throw new NotImplementedException();

        public IDictionary<string, string> ValidationErrors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        List<string> IResult.Errors => throw new NotImplementedException();
    }
}
