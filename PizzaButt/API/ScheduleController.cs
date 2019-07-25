using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.Service;
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
        private readonly ILocationService _locationService;
        private readonly IScheduleService _scheduleService;

        public ScheduleController(CathedralKitchenContext ctx, ILocationService locationService, IScheduleService scheduleService)
        {
            _ctx = ctx;
            _locationService = locationService;
            _scheduleService = scheduleService;
        }

        [HttpGet("")]
        public IActionResult GetScheduling()
        {
            var filteredScheduleConfigViewModel = _scheduleService.GetScheduledCommunities();

            return Ok(filteredScheduleConfigViewModel);
        }

        [HttpGet("communities")]
        public IActionResult GetScheduledCommunities()
        {
            var scheduledCommunties = _scheduleService.GetTodaysScheduledCommunities();

            return Ok(scheduledCommunties);
        }



    }
}