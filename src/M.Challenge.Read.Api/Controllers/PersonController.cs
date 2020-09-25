using M.Challenge.Read.Api.Infrastructure.Auth.Policies;
using M.Challenge.Read.Domain.Entities;
using M.Challenge.Read.Domain.Services.Person.Fetch;
using M.Challenge.Read.Domain.Services.Person.Search;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace M.Challenge.Read.Api.Controllers
{
    [Route("odata/person")]
    [ApiController]
    [Authorize(Policy = Policies.Reading)]
    public class PersonController : ControllerBase
    {
        public IFetchPersonService FetchPersonService { get; }
        public ISearchPersonService SearchPersonService { get; }

        public PersonController(
            IFetchPersonService fetchPersonService,
            ISearchPersonService searchPersonService)
        {
            FetchPersonService = fetchPersonService ?? throw new ArgumentNullException(nameof(fetchPersonService));
            SearchPersonService = searchPersonService ?? throw new ArgumentNullException(nameof(searchPersonService));
        }

        [HttpGet]
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        public List<Person> Search()
        {
            return SearchPersonService.Process();
        }

        [HttpGet]
        [EnableQuery]
        [Route("{id}")]
        public Person Fetch(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            return FetchPersonService.Process(id);
        }
    }
}
