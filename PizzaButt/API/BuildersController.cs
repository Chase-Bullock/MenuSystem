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
    [Route("api")]
    public class BuildersController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly IBuilderService _builderService;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public BuildersController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx, IBuilderService builderService)
        {
            _ctx = ctx;
            _builderService = builderService;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpGet("builders")]
        public IActionResult GetBuilders()
        {
            var data = _builderService.GetBuilders();


            return Ok(data);
        }

    }
}
