using M.Challenge.Read.Api.Infrastructure.Attributes;
using M.Challenge.Read.Api.Infrastructure.Auth.Policies;
using M.Challenge.Read.Api.Infrastructure.Response;
using M.Challenge.Read.Domain.Contracts.Request;
using M.Challenge.Read.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace M.Challenge.Read.Api.Controllers
{
    [Route("person")]
    [ApiController]
    [Authorize(Policy = Policies.Reading)]
    public class PersonController : ControllerBase
    {
        public IResponseFactory ResponseFactory { get; }

        public PersonController(IResponseFactory responseFactory)
        {
            ResponseFactory = responseFactory ?? throw new System.ArgumentNullException(nameof(responseFactory));
        }

        [HttpGet]
        [ModelStateValidator]
        public async Task<IActionResult> GetPerson([FromBody] PersonRequest contract)
        {
            return ResponseFactory
                .Return(new QueryResultDto().InvalidContract());
        }
    }
}
