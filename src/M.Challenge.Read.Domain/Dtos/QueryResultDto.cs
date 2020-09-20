using M.Challenge.Read.Domain.Constants;
using System.Diagnostics.CodeAnalysis;

namespace M.Challenge.Read.Domain.Dtos
{
    [ExcludeFromCodeCoverage]
    public class QueryResultDto : ResultDto
    {
        public QueryResultDto()
        {
        }

        public QueryResultDto(ReturnType returnType, object data, string message)
        {
            ReturnType = returnType;
            Data = data;
            Message = message;
        }

        public QueryResultDto NoContent()
        {
            ReturnType = ReturnType.NoContent;
            Message = ErrorMessageConstants.NoContent;
            return this;
        }
    }
}
