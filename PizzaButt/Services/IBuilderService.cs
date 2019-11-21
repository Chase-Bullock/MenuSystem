using System.Collections.Generic;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface IBuilderService
    {
        List<BuilderViewModel> GetBuilders();
    }
}