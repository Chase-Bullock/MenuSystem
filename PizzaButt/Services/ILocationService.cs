using System.Collections.Generic;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface ILocationService
    {
        List<CityViewModel> GetCities();
        List<CommunityViewModel> GetCommunities();
        List<CommunityViewModel> GetTodaysCommunities();
        void MissingCommunity(string communityName);
    }
}