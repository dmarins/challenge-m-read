using M.Challenge.Read.Api.Infrastructure.Auth.Policies;
using M.Challenge.Read.Api.Infrastructure.Response;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace M.Challenge.Read.Api.Controllers
{
    [Route("odata/person")]
    [ApiController]
    [Authorize(Policy = Policies.Reading)]
    public class PersonController : ControllerBase
    {
        public IResponseFactory ResponseFactory { get; }

        public PersonController(IResponseFactory responseFactory)
        {
            ResponseFactory = responseFactory ?? throw new System.ArgumentNullException(nameof(responseFactory));
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetPerson()
        {
            var rng = new Random();

            return Ok(Enumerable.Range(1, 5).Select(index => new
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
