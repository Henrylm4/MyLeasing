using MyLeasing.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyLeasing.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetTokenAsync(
            string urlBase,//url donde publicamos
            string servicePrefix,//Api prefijo
            string controller,//Controlador de la aaccion
            TokenRequest request)// el usuario y contrase;a
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(request);//serializa el usuario y contrasea en json
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{servicePrefix}{controller}";//concatena el prefijo con el contralador
                var response = await client.PostAsync(url, content);// dice si es un post, get, put, etc. en este caso POST
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)// si el usuario y contrase;a no son validos entonces...
                {
                    return new Response //se retorna una respesta de que no hubo login
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var token = JsonConvert.DeserializeObject<TokenResponse>(result);//se deserializa en token del json
                return new Response
                {
                    IsSuccess = true,
                    Result = token
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> GetOwnerByEmail(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email)
        {
            try
            {
                var request = new EmailRequest { Email = email };
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var owner = JsonConvert.DeserializeObject<OwnerResponse>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = owner
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }

}
