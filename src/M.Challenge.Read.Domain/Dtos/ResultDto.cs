using M.Challenge.Read.Domain.Constants;
using System.Diagnostics.CodeAnalysis;

namespace M.Challenge.Read.Domain.Dtos
{
    [ExcludeFromCodeCoverage]
    public class ResultDto
    {
        public ReturnType ReturnType { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        public ResultDto Fail()
        {
            ReturnType = ReturnType.Fail;
            Message = ErrorMessageConstants.Fail;
            return this;
        }

        public ResultDto InvalidContract()
        {
            ReturnType = ReturnType.InvalidContract;
            Message = ErrorMessageConstants.InvalidContract;
            return this;
        }
    }

    public enum ReturnType
    {
        Undefined,
        Fail,
        InvalidContract,
        NoContent
    }
}
