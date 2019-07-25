﻿using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using CathedralKitchen.ViewModels.AccountViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CathedralKitchen.API
{
    public partial class ApiClient
    {
        public async Task<List<string>> GetScheduledCommunities()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Schedule/communities"));
            return await GetAsync<List<string>>(requestUrl);
        }

        public async Task<List<ScheduleConfigViewModel>> GetSchedule()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Schedule"));
            return await GetAsync<List<ScheduleConfigViewModel>>(requestUrl);
        }

        public async Task<PersonViewModel> UpdatePerson(long id, PersonViewModel person)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Person/" + id));
            return await PostAsync(requestUrl, person);
        }

        public async Task<PersonViewModel> CreatePerson(PersonViewModel person)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Person"));
            return await PostAsync(requestUrl, person);
        }

        public async Task<List<OrderViewModel>> GetStatusOfAllOrders()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Orders"));
            return await GetAsync<List<OrderViewModel>>(requestUrl);
        }

        public async Task<OrderViewModel> GetOrderInfoForCustomer(long orderId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Orders/"+ orderId));
            return await GetAsync<OrderViewModel>(requestUrl);
        }

        public async Task<PersonRegisterViewModel> Register(PersonRegisterViewModel person)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Accounts"));
            return await PostAsync(requestUrl, person);
        }

        public async Task<List<MenuItem>> GetMenuItems()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Menu"));
            return await GetAsync<List<MenuItem>>(requestUrl);
        }

        public async Task<string> MissingCommunity(string communityName)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Schedule/missingcommunities"));
            return await PostAsync(requestUrl, communityName);

        }


    }
}