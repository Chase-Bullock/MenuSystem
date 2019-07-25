using System.Collections.Generic;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface IScheduleService
    {
        List<ScheduleConfigViewModel> GetScheduledCommunities();
        List<string> GetTodaysScheduledCommunities();
    }
}