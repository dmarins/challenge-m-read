using M.Challenge.Read.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace M.Challenge.Read.Api.Infrastructure.Response
{
    public interface IResponseFactory
    {
        ObjectResult Return(ResultDto dto);
    }
}
