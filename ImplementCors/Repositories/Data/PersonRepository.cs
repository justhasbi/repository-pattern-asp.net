
using ImplementCors.Base;
using Microsoft.AspNetCore.Http;
using NETCore.Models;
using NETCore.ViewModel;
using NETCore.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImplementCors.Repositories.Data
{
    public class PersonRepository : GeneralRepository<Person, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor contextAccessor;

        public PersonRepository(Address address, string request = "Persons/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<PersonVM>> GetAllPersons()
        {
            List<PersonVM> entities = new List<PersonVM>();

            using (var response = await httpClient.GetAsync(request + "getperson"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<PersonVM>>(apiResponse);
            }
            return entities;
        }

        // get by id
        public async Task<PersonVM> GetPersonByNik(string id)
        {
            PersonVM entity = null;

            using (var response = await httpClient.GetAsync(request + "getperson/" +  id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<PersonVM>(apiResponse);
            }
            return entity;
        }
    }
}
