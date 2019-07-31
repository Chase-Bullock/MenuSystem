using System.Collections.Generic;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface ILocationService
    {
        List<CommunityViewModel> GetCommunities();
        void MissingCommunity(string communityName);
    }
}