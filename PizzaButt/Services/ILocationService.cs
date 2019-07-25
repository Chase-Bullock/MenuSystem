using System.Collections.Generic;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface ILocationService
    {
        List<CommunityViewModel> GetCommunities();
        MenuItem GetItemById(long id);
        void MissingCommunity(string communityName);
    }
}