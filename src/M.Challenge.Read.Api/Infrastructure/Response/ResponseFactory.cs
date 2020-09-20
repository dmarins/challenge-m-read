using M.Challenge.Read.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace M.Challenge.Read.Api.Infrastructure.Response
{
    public class ResponseFactory : IResponseFactory
    {
        public ObjectResult Return(ResultDto dto)
        {
            switch (dto.ReturnType)
            {
                default:
                case ReturnType.Undefined:
                    return
                        new ObjectResult(dto.Message)
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest
                        };
                case ReturnType.Fail:
                    return
                        new ObjectResult(dto.Message)
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError
                        };
                case ReturnType.InvalidContract:
                    return
                        new ObjectResult(dto.Message)
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest
                        };
                case ReturnType.Created:
                    return
                         new ObjectResult(dto.Data)
                         {
                             StatusCode = (int)HttpStatusCode.Created
                         };
            }
        }
    }
}
