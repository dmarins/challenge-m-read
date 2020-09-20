using System.Diagnostics.CodeAnalysis;

namespace M.Challenge.Read.Domain.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CommandResultDto : ResultDto
    {
        public CommandResultDto()
        {
        }

        public CommandResultDto(ReturnType returnType, object data, string message)
        {
            ReturnType = returnType;
            Data = data;
            Message = message;
        }

        public CommandResultDto Created(object data = null)
        {
            ReturnType = ReturnType.Created;
            Data = data;
            return this;
        }
    }
}
