using System;
using System.Text;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;
using alexmelk.LentaRss.Models;

namespace alexmelk.LentaRss.Services
{
    public class HttpClientProxy : IHttpClientProxy
    {
        private string _apiUrl;
        private string _serviceName;

        public HttpClientProxy(string apiUrl, string serviceName)
        {
            _apiUrl = apiUrl;
            _serviceName = serviceName;
        }

        public async Task<ResultModel> SendPost(string content, string path)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(15000);

                var http = new System.Net.Http.HttpClient();

                var param = new StringContent(content: content, encoding: Encoding.UTF8, mediaType: "application/json");
                var answer = await http.PostAsync(requestUri: $"{_apiUrl}{path}", content: param, cancellationToken: cts.Token);

                var str = await answer.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject(str);

                var result = new ResultModel { Result = true, Data = obj };

                return result;
            }
            catch (Exception e)
            {
                return ExceptionHandler(e);
            }
        }

        public async Task<ResultModel> SendGet(string path, bool deserialize = true)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(15000);

                var http = new System.Net.Http.HttpClient();

                var answer = await http.GetAsync(requestUri: $"{_apiUrl}{path}", cancellationToken: cts.Token);

                var str = await answer.Content.ReadAsStringAsync();
                var obj = deserialize? JsonConvert.DeserializeObject(str):str;

                var result = new ResultModel { Result = true, Data = obj };

                return result;
            }
            catch (Exception e)
            {
                return ExceptionHandler(e);
            }
        }

        private ResultModel ExceptionHandler(Exception e)
        {
            string errorText = "";
            switch (e.Message)
            {
                case "Этот хост неизвестен.":
                    {
                        errorText = $"Не удалось связаться с сервисом <b>{_serviceName}</b> :((";
                        break;
                    }
                default:
                    {
                        errorText = e.Message;
                        break;
                    }
            }

            return new ResultModel { Result = false, InformationText = errorText, ShowInformation = false };
        }
    }
}
