using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public LocationsController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpGet]
        public IActionResult GetCommunities()
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


            return Json(data);
        }


        [HttpGet("{id}")]
        public IActionResult GetItemById(long id)
        {
            var data = _ctx.MenuItem.Where(x => x.Active == true && x.Id == id);

            return Json(data);
        }

    }
}
