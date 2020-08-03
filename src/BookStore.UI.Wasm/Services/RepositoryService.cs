using BookStore.UI.Wasm.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BookStore.UI.Wasm.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly HttpClient _client;
        //private readonly ILocalStorageService _localStorage;

        public RepositoryService(HttpClient client)
        {
            _client = client;
            // , ILocalStorageService localStorage _localStorage = localStorage;
        }

        public async Task<T> Create(string url, T entity)
        {
           // _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", await GetBearertoken());

            HttpResponseMessage response = await _client.PostAsJsonAsync<T>(url, entity);
            
            if (response.StatusCode == System.Net.HttpStatusCode.Created) 
                return entity;

            return null;  
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id < 1) 
                return false;

            HttpResponseMessage response = await _client.DeleteAsync(url + id);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;

            return false;
        }

        public async Task<IList<T>> GetAll(string url)
        {
            var response = await _client.GetFromJsonAsync<IList<T>>(url);

            return response;
        }

        public async Task<T> GetSingle(string url, int id)
        {
            var response = await _client.GetFromJsonAsync<T>(url + id);

            return response;
        }

        public async Task<T> Update(string url, T entity, int id)
        {
            if (entity == null) return null;

            var response = await _client.PutAsJsonAsync<T>(url + id, entity);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return entity;
            return null;
        }

        //private async Task<string> GetBearerToken()
        //{
        //    return await _localStorage.GetItemAsync<string>("authToken");
        //}
    }
}
