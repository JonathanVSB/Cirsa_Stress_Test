using Cirsa_Stress_Test_Progam.Views;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Cirsa_Stress_Test_Progam.Models
{
    public class RequestSender
    {
        private readonly HttpClient httpClient;
        private readonly View view;

        public RequestSender()
        {
            httpClient = new HttpClient();
            this.view = new View();
        }

        /// <summary>
        /// Executes Get Request to /gamedata endpoint
        /// </summary>
        /// <returns></returns>
        public async Task executeGetRequest(TestType type)
        {
            Metrics m = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Does an asynchronous HTTP GET Request
                    HttpResponseMessage response = await client.GetAsync(EndPoint.GetGameData);
                   

                    // Shows a message depending of the status
                    //return view.successStatus(response.IsSuccessStatusCode);
                    stopwatch.Stop();
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    // Create Metrics object to save necessary data to analise
                    double time = elapsedTime.TotalSeconds;
                    string description = view.successStatus(response.IsSuccessStatusCode);
                    int code = (int)response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var games = JsonConvert.DeserializeObject<List<ResponseObject>>(responseBody);

                        //Fufill the list with games
                        if (Controller.Controller.existingGameIds.Count <= 0)
                        {

                            foreach (var game in games)
                            {
                                Controller.Controller.existingGameIds.Add(game.Id);
                            }
                        }
                        else
                        {
                            foreach (var game in games)
                            {
                                Controller.Controller.newGameIds.Add(game.Id);
                            }
                        }

                        switch (type)
                        {

                            case TestType.Smoke:

                                PDFMaker.testSmokeMetrics.Add(new Metrics("Smoke",time, description, "GET", code));
                                break;

                            case TestType.Average:
                                PDFMaker.testAverageMetrics.Add(new Metrics("Average", time, description, "GET", code));
                                break;

                            case TestType.Spike:
                                PDFMaker.testSpikeMetrics.Add(new Metrics("Spike", time, description, "GET", code));
                                break;
                        }


                    }
                }
                catch (Exception x)
                {
                    view.displayMessage($" [Error]: {x.Message}");

                }
            }
        }

        /// <summary>
        /// Executes Get Rquest to /gamedata{Id} to get data by given Id
        /// </summary>
        /// <param name="id"></param> Id to complete the endpoint
        /// <returns></returns>
        public async Task executeGetRequestById(TestType type, Guid id)
        {
            Metrics m = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Does an asynchronous HTTP GET Request to get data by Id
                    HttpResponseMessage response = await client.GetAsync(EndPoint.GetGameDataById(id.ToString()));

                    stopwatch.Stop();
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    // Create Metrics object to save necessary data to analise
                    double time = elapsedTime.TotalSeconds;
                    string description = view.successStatus(response.IsSuccessStatusCode);
                    int code = (int)response.StatusCode;

                    switch (type)
                    {
                        case TestType.Smoke:
                            PDFMaker.testSmokeMetrics.Add(new Metrics("Smoke", time, description, "GET", code));
                            break;

                        case TestType.Average:
                            PDFMaker.testAverageMetrics.Add(new Metrics("Average", time, description, "GET", code));
                            break;

                        case TestType.Spike:
                            PDFMaker.testSpikeMetrics.Add(new Metrics("Spike", time, description, "GET", code));
                            break;
                    }
                }
                catch (Exception x)
                {
                    view.displayMessage($" [Error]: {x.Message}");

                }
            }
        }

        /// <summary>
        /// Modifies an existing game from database and by his Id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task executePutRequest(TestType type, Guid id)
        {
            Metrics m = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(new ResponseObject("Yugioh Card", "Card Game"));
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(EndPoint.GetGameDataById(id.ToString()), content);

                    stopwatch.Stop();
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    // Create Metrics object to save necessary data to analise
                    double time = elapsedTime.TotalSeconds;
                    string description = view.successStatus(response.IsSuccessStatusCode);
                    int code = (int)response.StatusCode;
                    switch (type)
                    {
                        case TestType.Smoke:
                            PDFMaker.testSmokeMetrics.Add(new Metrics("Smoke",time, description, "PUT", code));
                            break;

                        case TestType.Average:
                            PDFMaker.testAverageMetrics.Add(new Metrics("Average", time, description, "PUT", code));
                            break;

                        case TestType.Spike:
                            PDFMaker.testSpikeMetrics.Add(new Metrics("Spike", time, description, "PUT", code));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    view.displayMessage($"{ex.Message}");

                }
            }
        }
        /// <summary>
        /// Executes POST Request to add a new obj to database
        /// </summary>
        /// <param name="obj"></param> obj to be added to database
        /// <returns></returns>
        public async Task executePostRequest(TestType type, ResponseObject obj)
        {
            Metrics m = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonData = JsonConvert.SerializeObject(obj);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(EndPoint.GetGameData, content);

                    stopwatch.Stop();
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    // Create Metrics object to save necessary data to analise
                    double time = elapsedTime.TotalSeconds;
                    string description = view.successStatus(response.IsSuccessStatusCode);
                    int code = (int)response.StatusCode;
                    switch (type)
                    {
                        case TestType.Smoke:
                            PDFMaker.testSmokeMetrics.Add(new Metrics("Smoke", time, description, "POST", code));
                            break;

                        case TestType.Average:
                            PDFMaker.testAverageMetrics.Add(new Metrics("Average", time, description, "POST", code));
                            break;

                        case TestType.Spike:
                            PDFMaker.testSpikeMetrics.Add(new Metrics("Spike", time, description,"POST", code));
                            break;
                    }
                }
                catch (Exception x)
                {
                    view.displayMessage($" [Error]: {x.Message}");

                }
            }
        }

        /// <summary>
        /// Executes DELETE Request to remove an obj from database
        /// </summary>
        /// <param name="id"></param> Id of obj to remove
        /// <returns></returns>
        public async Task executeDeleteRequest(TestType type, Guid id)
        {
            Metrics m = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(EndPoint.GetGameDataById(id.ToString()));

                    stopwatch.Stop();
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    // Create Metrics object to save necessary data to analise
                    double time = elapsedTime.TotalSeconds;
                    string description = view.successStatus(response.IsSuccessStatusCode);
                    int code = (int)response.StatusCode;
                    switch (type)
                    {
                        case TestType.Smoke:
                            PDFMaker.testSmokeMetrics.Add(new Metrics("Smoke", time, description, "DELETE", code));
                            break;

                        case TestType.Average:
                            PDFMaker.testAverageMetrics.Add(new Metrics("Average", time, description, "DELETE", code));
                            break;

                        case TestType.Spike:
                            PDFMaker.testSpikeMetrics.Add(new Metrics("Spike", time, description, "DELETE", code));
                            break;
                    }
                }
                catch (Exception x)
                {
                    view.displayMessage($" [Error]: {x.Message}");

                }
            }
        }
    }


}



