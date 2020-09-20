using M.Challenge.Read.Domain.Dtos;
using M.Challenge.Read.Domain.Logger;
using M.Challenge.Read.Domain.Services.Person;
using System;
using System.Threading.Tasks;

namespace M.Challenge.Read.Application.Services.Person.Add
{
    public class AddPersonExceptionHandler : IAddPersonService
    {
        public ILogger Logger { get; }
        public IAddPersonService Decorated { get; set; }

        public AddPersonExceptionHandler(ILogger logger, IAddPersonService decorated)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        public async Task<ResultDto> Process(PersonCrudDto dto)
        {
            try
            {
                return await Decorated.Process(dto);
            }
            catch (Exception e)
            {
                Logger.Error("Erro no cadastro de pessoa.", e);
                return new CommandResultDto().Fail();
            }
        }
    }
}
