using System.Threading.Tasks;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface IPersonService
    {
        Task<long> CreatePerson(PersonViewModel person);
        Task<long> UpdatePerson(PersonViewModel request, long id);
    }
}