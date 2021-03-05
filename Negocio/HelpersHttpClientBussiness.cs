using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DaemonIndustialMolinera.Negocio
{
    public class HelpersHttpClientBussiness
    {
        private string BaseUrl = "http://pedido.gnoboa.com:2108/api/";
#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<List<T>> GetApi<T>(string API) where T : class
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BaseUrl);
                //HTTP GET
                var responseTask = client.GetAsync(API);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<T>>();
                    readTask.Wait();

                    List<T> _data = readTask.Result;
                    return _data;

                }
            }
            return null;
        }
#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<bool> PostApi(string API, string payload)
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            using (var client = new HttpClient())
            {
                //  HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseUrl);
                //HTTP GET
                var responseTask = client.PostAsync(API, stringContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<bool>();
                    readTask.Wait();

                    bool _data = readTask.Result;
                    return _data;

                }
            }
            return false;
        }
#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<bool> GettApiParam(string API)
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            using (var client = new HttpClient())
            {
                //  HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                //  var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseUrl);
                //HTTP GET
                var responseTask = client.GetAsync(API);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<bool>();
                    readTask.Wait();

                    bool _data = readTask.Result;
                    return _data;

                }
            }
            return false;
        }
    }
}
