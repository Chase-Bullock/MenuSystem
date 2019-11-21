using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.Service
{
    public class LocationService : ILocationService
    {
        private readonly CathedralKitchenContext _ctx;

        public LocationService(CathedralKitchenContext ctx)
        {
            _ctx = ctx;
        }

        public List<CommunityViewModel> GetCommunities()
        {
            var data = new List<CommunityViewModel>();

            var communities = _ctx.Community.Where(x => x.Active == true);

            foreach (var community in communities)
            {
                var communityViewModel = new CommunityViewModel
                {
                    Id = community.Id,
                    Name = community.Name
                };
                data.Add(communityViewModel);
            }


            return data;
        }

        public List<CommunityViewModel> GetTodaysCommunities()
        {
            var todaysSchedule = _ctx.ScheduleConfig.Include(y => y.Community).Where(x => x.Date.Date == DateTime.Today);
            var todaysCommunities = new List<CommunityViewModel>();
            var scheduledCommunties = new List<string>();

            foreach (var config in todaysSchedule)
            {
                var communityViewModel = new CommunityViewModel
                {
                    Id = config.Community.Id,
                    Name = config.Community.Name
                };
                todaysCommunities.Add(communityViewModel);
            };


            return todaysCommunities;
        }

        public List<CityViewModel> GetCities()
        {
            var data = new List<CityViewModel>();

            var cities = _ctx.City.Where(x => x.Active == true);

            foreach (var city in cities)
            {
                var cityViewModel = new CityViewModel
                {
                    Id = city.Id,
                    Name = city.Name
                };
                data.Add(cityViewModel);
            }


            return data;
        }

        public void MissingCommunity(string communityName)
        {
            _ctx.Add(new CommunityRequest
            {
                Name = communityName,
                Active = true,
                CreateTime = DateTime.UtcNow
            });

            _ctx.SaveChanges();

        }


    }
}
