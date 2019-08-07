using Swashbuckle.AspNetCore.Swagger;

namespace CathedralKitchen
{
    internal class OpenApiInfo : Info
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}