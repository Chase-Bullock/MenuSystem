using CathedralKitchen.NewModels;

namespace CathedralKitchen
{
    public interface IEmailNotificationService
    {
        void SendMail(Person person, int status);
    }
}