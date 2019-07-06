using CathedralKitchen.API;
using CathedralKitchen.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CathedralKitchen.Factory
{
    internal static class ApiClientFactory
    {
        private static Uri _apiUri;

        private static Lazy<ApiClient> restClient = new Lazy<ApiClient>(
            () => new ApiClient(_apiUri),
            LazyThreadSafetyMode.ExecutionAndPublication);
        static ApiClientFactory()
        {
            _apiUri = new Uri(ApplicationSettings.WebApiUrl);
        }

        public static ApiClient Instance
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}
