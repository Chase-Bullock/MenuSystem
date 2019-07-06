using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public ScheduleController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpGet("")]
        public IActionResult GetScheduling()
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


            return Ok(filteredScheduleConfigViewModel);
        }

        [HttpGet("communities")]
        public IActionResult GetScheduledCommunities()
        {
            var todaysSchedule = _ctx.ScheduleConfig.Include(y => y.Community).Where(x => x.Date.Date == DateTime.Today);
            var scheduledCommunties = new List<string>();

            foreach (var config in todaysSchedule)
            {
                scheduledCommunties.Add(config.Community.Name);
            };

            return Ok(scheduledCommunties);
        }


        [HttpGet("{id}")]
        public IActionResult GetItemById(long id)
        {
            var data = _ctx.MenuItem.Where(x => x.Active == true && x.Id == id);

            return Json(data);
        }

    }

    public class ScheduleReturnData
    {
        public List<ScheduleConfigViewModel> scheduleConfigs { get; set; }
        public List<string> scheduledCommunities { get; set; }
    }
}
