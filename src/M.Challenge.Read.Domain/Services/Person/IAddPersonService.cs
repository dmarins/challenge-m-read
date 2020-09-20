using M.Challenge.Read.Domain.Dtos;
using System.Threading.Tasks;

namespace M.Challenge.Read.Domain.Services.Person
{
    public interface IAddPersonService
    {
        Task<ResultDto> Process(PersonCrudDto dto);
    }
}
