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
    public class ScheduleService : Controller, IScheduleService
    {
        private readonly CathedralKitchenContext _ctx;

        public ScheduleService(CathedralKitchenContext ctx)
        {
            _ctx = ctx;
        }

        public List<ScheduleConfigViewModel> GetScheduledCommunities()
        {
            var todaysSchedule = _ctx.ScheduleConfig.Include(y => y.Community).Where(x => x.Date.Date == DateTime.Today);
            var filteredScheduleConfigViewModel = new List<ScheduleConfigViewModel>();
            var scheduledCommunties = new List<string>();

            foreach (var config in todaysSchedule)
            {
                var communityViewModel = new CommunityViewModel
                {
                    Id = config.Community.Id,
                    Name = config.Community.Name
                };

                var scheduleViewModel = new ScheduleConfigViewModel
                {
                    Id = config.Id,
                    //Builder = builderViewModel,
                    Community = communityViewModel,
                    Date = config.Date,
                    Active = config.Active
                };

                filteredScheduleConfigViewModel.Add(scheduleViewModel);
            };


            return filteredScheduleConfigViewModel;
        }

        public List<string> GetTodaysScheduledCommunities()
        {
            var todaysSchedule = _ctx.ScheduleConfig.Include(y => y.Community).Where(x => x.Date.Date == DateTime.Today);
            var scheduledCommunties = new List<string>();

            foreach (var config in todaysSchedule)
            {
                scheduledCommunties.Add(config.Community.Name);
            };

            return scheduledCommunties;
        }



    }
}