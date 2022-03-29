using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcLogicApps.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcLogicApps.Services
{
    public class ServiceLogicApps
    {
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceLogicApps()
        {
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task SendMailAsync
            (string email, string subject, string body)
        {
            string urlMail =
                "https://prod-166.westeurope.logic.azure.com:443/workflows/7ca93169c9784542b0c757c8c3c6e898/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=vKHU8c_2C9Npn4bWFDhi717SPnRFuSZV1G4l4kZRp8s";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                EmailModel emailModel = new EmailModel
                {
                     Email = email, Subject = subject, Body = body
                };
                //CONVERTIMOS EL MODEL A JSON
                string json = JsonConvert.SerializeObject(emailModel);
                //LA PETICIONES POST Y RECIBE LA INFORMACION EN JSON
                //MEDIANTE STRINGCONTENT
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(urlMail, content);
            }
        }

        public async Task<string> SumarNumerosAsync(int numero1, int numero2)
        {
            string urlFlowSuma =
                "https://prod-69.westeurope.logic.azure.com:443/workflows/eee588f078d249c793d2ad51c3a1d9ee/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=3goBe3PPMpPbRuTw1P6-cRbE_oZNs2-TyEmywLU2Sts";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var sumaModel = new
                {
                    Numero1 = numero1, Numero2 = numero2
                };
                var json = JsonConvert.SerializeObject(sumaModel);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(urlFlowSuma, content);
                if (response.IsSuccessStatusCode)
                {
                    string data =
                        await response.Content.ReadAsStringAsync();
                    return "La suma es " + data;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Tabla>> TablaMultiplicarAsync(int numero)
        {
            string urlTabla =
                "https://prod-109.westeurope.logic.azure.com:443/workflows/040233c6dd1b4bceb1fdbf64027c5e57/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=ROzEZIbPoZM4V2GPP9qVnHgpnx_BjqdH3RbB7CLZiuU";
            using (HttpClient client = new HttpClient())
            {
                var modelNumero = new
                {
                    Numero = numero
                };
                var json = JsonConvert.SerializeObject(modelNumero);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(urlTabla, content);
                if (response.IsSuccessStatusCode)
                {
                    List<Tabla> tabla =
                        await response.Content.ReadAsAsync<List<Tabla>>();
                    return tabla;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task InsertarDoctorAsync(Doctor doctor)
        {
            string urlFlowInsert = "https://prod-89.westeurope.logic.azure.com:443/workflows/1735e64bd9034aecbf3f6f6ab2927792/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=E5OBCUK8FHWbBuspRFGvhqlwbJ1gA1sFIFCnAZMUPnw";
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(doctor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlFlowInsert, content);
            }
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string urlFlowDoctores = "https://prod-221.westeurope.logic.azure.com:443/workflows/73b653dda2164b4ba718ae214ee4865e/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=qbzjcvFyuIt7QIfa0IRXQe_6LCTyUfF_dlypd_fQ45E";
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.PostAsync(urlFlowDoctores, null);
                if (response.IsSuccessStatusCode)
                {
                    List<Doctor> doctors = await response.Content.ReadAsAsync<List<Doctor>>();
                    return doctors;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Doctor> FindDoctorAsync(int idDoctor)
        {
            string urlFlowDetail =
            "https://prod-178.westeurope.logic.azure.com:443/workflows/69a224c2720b46349f431204c05d3b76/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=T1JPGXLlEmpX6UZn9gV-9cNJA6mJ8jmv9EAiBse4Fuc";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var modelId = new { IdDoctor = idDoctor };
                string json = JsonConvert.SerializeObject(modelId);
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PostAsync(urlFlowDetail, content);
                if (response.IsSuccessStatusCode)
                {
                    Doctor doctor = await response.Content.ReadAsAsync<Doctor>();
                    return doctor;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<string> AnalizarComentarioAsync(string comentario)
        {
            string urlFlowSentimientos = "https://prod-77.westeurope.logic.azure.com:443/workflows/1420e68b4064442dbd82ea5ca4f04f6a/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=SbzIpl_M4p2ySi7XYJ9UYNIsfCFwLhmDkRGixuV9caQ";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var modelComentario = new { Comentario = comentario };
                string json = JsonConvert.SerializeObject(modelComentario);
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PostAsync(urlFlowSentimientos, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    return "Algo está fallando...";
                }
            }
        }
    }
}
