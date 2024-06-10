using Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RazorPages.Pages.Admin.Products
{
    public class ProductsModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public ProductsModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public IList<Product> Products { get; private set; }

        public async Task OnGetAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync($"{_configuration["RestApiBaseUrl"]}/api/products");

            if (response.IsSuccessStatusCode)
            {
                Products = await response.Content.ReadFromJsonAsync<IList<Product>>();
            }
            else
            {
                // Handle error
            }
        }

    }
}
