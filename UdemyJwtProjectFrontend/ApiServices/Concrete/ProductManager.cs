using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UdemyJwtProjectFrontend.ApiServices.Interfaces;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.ApiServices.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IHttpContextAccessor _accessor;

        public ProductManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task AddAsync(ProductAdd productAdd)
        {
            var token = _accessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(productAdd);

                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await httpClient.PostAsync("http://localhost:60531/api/products" , stringContent);
            }
        }

        public async Task<List<ProductList>> GetAllAsync()
        {
            var token = _accessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await httpClient.GetAsync("http://localhost:60531/api/products");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var products = JsonConvert.DeserializeObject<List<ProductList>>(await responseMessage.Content.ReadAsStringAsync());

                    return products;
                }
            }

            return null;
        }

        public async Task<ProductList> GetByIdAsync(int id)
        {
            var token = _accessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await httpClient.GetAsync($"http://localhost:60531/api/products/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var product = JsonConvert.DeserializeObject<ProductList>(await responseMessage.Content.ReadAsStringAsync());

                    return product;
                }
            }

            return null;
        }

        public async Task UpdateAsync(ProductList productList)
        {
            var token = _accessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(productList);

                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await httpClient.PutAsync($"http://localhost:60531/api/products", stringContent);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var token = _accessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                await httpClient.DeleteAsync($"http://localhost:60531/api/products/{id}");
            }
        }
    }
}