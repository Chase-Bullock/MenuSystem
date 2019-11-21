using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.Service;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ILocationService _locationService;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public LocationsController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx, ILocationService locationService)
        {
            _ctx = ctx;
            _locationService = locationService;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpGet("communities")]
        public IActionResult GetCommunities()
        {
            var data = _locationService.GetCommunities();

            return Ok(data);
        }

        [HttpGet("communities/today")]
        public IActionResult GetTodaysCommunities()
        {
            var data = _locationService.GetTodaysCommunities();

            return Ok(data);
        }

        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            var data = _locationService.GetCities();

            return Ok(data);
        }

        [HttpPost("communities/missing/{communityName}")]
        public IActionResult MissingCommunity(string communityName)
        {
            _locationService.MissingCommunity(communityName);

            return Ok();
        }

    }
}
