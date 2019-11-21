using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CathedralKitchen.Service
{
    public class BuilderService : IBuilderService
    {
        private readonly CathedralKitchenContext _ctx;

        public BuilderService(CathedralKitchenContext ctx)
        {
            _ctx = ctx;
        }

        public List<BuilderViewModel> GetBuilders()
        {
            var data = new List<BuilderViewModel>();

            var builders = _ctx.Builder.Where(x => x.Active == true);

            foreach (var builder in builders)
            {
                var builderViewModel = new BuilderViewModel
                {
                    Id = builder.Id,
                    Name = builder.Name
                };
                data.Add(builderViewModel);
            }


            return data;
        }
    }
}
