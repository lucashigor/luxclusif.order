using Bogus;
using FluentAssertions;
using luxclusif.order.application.Models;
using luxclusif.order.domain.Entity;
using luxclusif.order.infrastructure.Repositories.Context;
using luxclusif.order.webapi;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace luxclusif.order.tests.Components.Utils
{
    public class IntegrationTestFixture : CustomWebApplicationFactory<Startup>
    {

        public readonly string MicroServiceName = "luxclusif.order.webapi";

        public readonly string EventName = "topic.createdorder";

        private readonly string apikey = "";

        public Faker Faker { get; set; }

        public DbContextOptions<PrincipalContext> DbContextOptions { get; }

        public IntegrationTestFixture()
        {
            DbContextOptions = new DbContextOptionsBuilder<PrincipalContext>()
                .UseInMemoryDatabase("IntegrationTestDatabase")
                .Options;

            Faker = new Faker();
        }

        public HttpClient CreateAuthorizedClient()
        {
            return CreateAuthorizedClient(null, null);
        }
        public HttpClient CreateAuthorizedClient(string token)
        {
            return CreateAuthorizedClient(token, null);
        }

        public HttpClient CreateAuthorizedClient(string token, string language)
        {
            HttpClient client = CreateClient();

            client.DefaultRequestHeaders.Add("apikey", apikey);

            if (!string.IsNullOrEmpty(language))
            {
                client.DefaultRequestHeaders.Add("Accept-Language", language);
            }
            else
            {
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            }

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }

            return client;
        }


        public void ResetDatabase()
        {
            using PrincipalContext context = new(DbContextOptions);
            context.Order.RemoveRange(context.Order);
            context.SaveChanges();

            context.Order.AddRange(new List<Order>()
            {
                new ("Wallet", Guid.NewGuid(), 1_223.33m)
            });

            context.SaveChanges();
        }


        public Task<T> GetWithValidations<T>(string url) where T : class
        {
            return GetWithValidations<T>(url, null, new());
        }

        public async Task<T> GetWithValidations<T>(string url, string code, List<ErrorModel> errorDetails) where T : class
        {
            HttpClient client;

            client = CreateAuthorizedClient();

            var response = await client.GetAsync(url);

            return await ValidateResponse<T>(code, errorDetails, response);
        }

        public Task<T> PostWithValidations<T>(string url, object data) where T : class
        {
            return PostWithValidations<T>(url, null, new(), data);
        }

        public async Task<T> PostWithValidations<T>(string url, string code, List<ErrorModel> errorDetails, object data) where T : class
        {
            HttpClient client;
            StringContent httpContent;

            client = CreateAuthorizedClient();

            Body(data, out httpContent);

            var response = await client.PostAsync(url, httpContent);

            return await ValidateResponse<T>(code, errorDetails, response);
        }

        private static void Body(object data, out StringContent httpContent)
        {
            string json = string.Empty;

            if (data != null)
            {
                json = JsonConvert.SerializeObject(data);
            }

            httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }

        private static async Task<T> ValidateResponse<T>(string code, List<ErrorModel> errorDetails, HttpResponseMessage response) where T : class
        {
            var content = await response.Content.ReadAsStringAsync();
            var ret = JsonConvert.DeserializeObject<DefaultResponseDto<T>>(content);

            ret.Should().NotBeNull();
            errorDetails.Should().Equals(ret?.Errors);

            return ret.Data!;
        }
    }
}
