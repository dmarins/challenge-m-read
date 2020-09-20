using M.Challenge.Read.Domain.Dtos;
using M.Challenge.Read.Domain.Logger;
using M.Challenge.Read.Domain.Repositories.Person;
using M.Challenge.Read.Domain.Services.Person;
using System;
using System.Threading.Tasks;

namespace M.Challenge.Read.Application.Services.Person.Add
{
    public class AddPersonValidator : IAddPersonService
    {
        public ILogger Logger { get; }
        public IPersonReadingRepository PersonReadingRepository { get; }
        public IAddPersonService Decorated { get; }

        public AddPersonValidator(
            ILogger logger,
            IPersonReadingRepository personReadingRepository,
            IAddPersonService decorated)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            PersonReadingRepository = personReadingRepository ?? throw new ArgumentNullException(nameof(personReadingRepository));
            Decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        public async Task<ResultDto> Process(PersonCrudDto dto)
        {
            var personStored = await PersonReadingRepository
                .GetBy(p => p.Name == dto.Name && p.LastName == dto.LastName);

            if (personStored != null)
            {
                Logger.Warning("Já existe um cadastro para esse mesmo nome e sobrenome.");
                return new CommandResultDto().InvalidContract();
            }

            return await Decorated.Process(dto);
        }
    }
}
