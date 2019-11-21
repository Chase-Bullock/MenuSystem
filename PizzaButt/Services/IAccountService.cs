using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels.AccountViewModels;

namespace CathedralKitchen.Service
{
    public interface IAccountService
    {
        Task<bool> ForgotPassword(ForgotPasswordViewModel model);
        Task<User> Login(LoginViewModel model);
        void Logout();
        Task<User> Register(RegisterViewModel model);
        void UpdateUser(PersonRegisterViewModel model, long id);
    }
}