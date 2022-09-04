using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

 namespace Transport

{

    public class HttpService
    {
        public static List<string> HTTP_METHODS = new List<string>() { "get", "post", "put", "patch", "delete", "head", "options", "delete" };


       

        public static string Rus(string eng) =>
            Request("get", @"https://translate.google.com/#view=home&op=translate&sl=en&tl=ru&text=" + eng);


        public static string Eng(string rus) =>
            Request("get", @"https://translate.google.com/#view=home&op=translate&sl=ru&tl=en&text=" + rus);


        public static async Task<string> RequestAsync(string method, string url)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }



        public static string Request(string method, string url)
        {
            if (url == null)
                throw new Exception("не указан обязательный параметр url (параметры чувствительны к регистру)");
            if (method == null)
                throw new Exception("не указан обязательный параметр method (параметры чувствительны к регистру)");
            if (!HTTP_METHODS.Contains(method.ToLower())) throw new Exception("не верно задан параметр method (параметры чувствительны к регистру) укажите один из: get, post, put, head, options or delete");
            Task<string> response = RequestAsync(method, url);
            response.Wait();
            string res = response.Result;
            Debug.WriteLine(res);
            return res;
        }

        public static async Task<int> TryRequest(string method, string url)
        {
            try
            {
                int code = await GetHttpStatus(method, url);
                return code;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"[{method}]{url}");
                Console.WriteLine($"[{ex.Message}]");
                return 0;
            }
        }

        private static async ValueTask<string> GetXrdsDocument(string method, string url){
            return await ValueTask.FromResult<string>(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>
                    <xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">

                        <xs:element name=""shiporder"">
                        <xs:complexType>
                            <xs:sequence>
                            <xs:element name=""orderperson"" type=""xs:string""/>
                            <xs:element name=""shipto"">
                                <xs:complexType>
                                <xs:sequence>
                                    <xs:element name=""name"" type=""xs:string""/>
                                    <xs:element name=""address"" type=""xs:string""/>
                                    <xs:element name=""city"" type=""xs:string""/>
                                    <xs:element name=""country"" type=""xs:string""/>
                                </xs:sequence>
                                </xs:complexType>
                            </xs:element>
                            <xs:element name=""item"" maxOccurs=""unbounded"">
                                <xs:complexType>
                                <xs:sequence>
                                    <xs:element name=""title"" type=""xs:string""/>
                                    <xs:element name=""note"" type=""xs:string"" minOccurs=""0""/>
                                    <xs:element name=""quantity"" type=""xs:positiveInteger""/>
                                    <xs:element name=""price"" type=""xs:decimal""/>
                                </xs:sequence>
                                </xs:complexType>
                            </xs:element>
                            </xs:sequence>
                            <xs:attribute name=""orderid"" type=""xs:string"" use=""required""/>
                        </xs:complexType>
                        </xs:element>

                    </xs:schema>");
    }

        private static async Task<int> GetHttpStatus(string method, string url)
        {
            if (url == null)
                throw new Exception("не указан обязательный параметр url (параметры чувствительны к регистру)");
            if (method == null)
                throw new Exception("не указан обязательный параметр method (параметры чувствительны к регистру)");
            if (!HTTP_METHODS.Contains(method.ToLower()))
                throw new Exception("не верно задан параметр method (параметры чувствительны к регистру) укажите один из: get, post, put, head, options or delete");

            System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();


            var services = new StringContent(await GetXrdsDocument(method, url),
                        Encoding.UTF8, "application/json-patch+json");

            switch (method.ToLower())
            {
                case "get":
                    var got = await _httpClient.GetAsync(url);
                    return (int)got.StatusCode;
                case "post":
                    var posted = await _httpClient.PostAsync(url, services);
                    return (int)posted.StatusCode;
                case "patch":
                    var patched = await _httpClient.PatchAsync(url, services);
                    return (int)patched.StatusCode;
                case "put":
                    var puted = await _httpClient.PatchAsync(url, services);
                    return (int)puted.StatusCode;
                case "options":
                    var configured = await _httpClient.PatchAsync(url, services);
                    return (int)configured.StatusCode;
                case "delete":
                    var deleted = await _httpClient.PatchAsync(url, services);
                    return (int)deleted.StatusCode;
                default:
                    throw new Exception("HTTP-метод указан некорректно, проверьте пож-та: method=" + method);
            }

        }


        /// <summary>
        /// Выполнение запроса
        /// </summary>  
        public static string Request(string json)
        {
            try
            {
                JObject options = JsonConvert.DeserializeObject<JObject>(json);
                if (!options.ContainsKey("url")) throw new Exception("не указан обязательный параметр url (параметры чувствительны к регистру)");
                if (!options.ContainsKey("method")) throw new Exception("не указан обязательный параметр method (параметры чувствительны к регистру)");
                string url = options["url"].Value<string>();
                string method = options["method"].Value<string>();
                if (!HTTP_METHODS.Contains(method)) throw new Exception("не верно задан параметр method (параметры чувствительны к регистру) укажите один из: get, post, put, head, options or delete");
                Task<string> response = RequestAsync(method, url);
                response.Wait();
                return response.Result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }

        }
    }

}