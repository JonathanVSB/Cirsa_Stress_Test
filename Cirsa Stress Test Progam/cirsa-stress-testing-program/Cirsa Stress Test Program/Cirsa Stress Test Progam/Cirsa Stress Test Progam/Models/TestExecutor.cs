using Cirsa_Stress_Test_Progam.Views;

namespace Cirsa_Stress_Test_Progam.Models
{
    public class TestExecutor
    {
        public event Action<double> OnProgressUpdate;

        private HttpClient httpClient;
        public View view;
        
        RequestSender sender;
        List<Guid> idsToDelete;

        public TestExecutor()
        {


            httpClient = new HttpClient();
            view = new View();
            idsToDelete = new List<Guid>();
            sender = new RequestSender();
        }

        /// <summary>
        /// Executes the test specified by test
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="switchOption"></param>
        /// <returns></returns>
        public async Task ExecuteTestAsync(TestType testType, int switchOption)
        {
            try
            {
                if (switchOption == 5)
                {
                    sender.executeGetRequest(testType);
                    if (Controller.Controller.newGameIds.Count>0)
                    {
                        idsToDelete = CreateUniqueNewGameIdList();
                    } 
                }
                switch (testType)
                {
                    case TestType.Smoke:
                        ExecuteStressTest(TestType.Smoke, switchOption);
                        break;
                    case TestType.Average:
                        ExecuteStressTest(TestType.Average, switchOption);
                        break;
                    case TestType.Spike:
                        ExecuteStressTest(TestType.Spike, switchOption);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(testType), $"Tipo de prueba no soportado: {testType}");
                }
            }
            catch (Exception ex)
            {
                view.displayMessage(ex.ToString());
            }

        }

        /// <summary>
        /// Launch the correct request method using TestTyoe and switchOption
        /// </summary>
        /// <param name="type"></param>
        /// <param name="switchOption"></param>
        /// <returns></returns>
        private async Task ExecuteStressTest(TestType type, int switchOption)
        {
            try
            {
                TimeSpan incrementInterval;
                TimeSpan rateIncrementInterval;
                int currentLoad;
                Random random = new Random();
                

                switch (type)
                {
                    case TestType.Smoke:
                        incrementInterval = TimeSpan.FromSeconds(1);
                        rateIncrementInterval = TimeSpan.FromSeconds(1);
                        currentLoad = 1;
                        break;

                    case TestType.Average:
                        incrementInterval = TimeSpan.FromMinutes(1);
                        rateIncrementInterval = TimeSpan.FromMinutes(0.5);
                        currentLoad = 50;
                        break;

                    case TestType.Spike:
                        incrementInterval = TimeSpan.FromMinutes(1);
                        rateIncrementInterval = TimeSpan.FromMinutes(0.5);
                        currentLoad = 500;
                        break;

                    default:
                        throw new ArgumentException("Tipo de test no válido");
                }

                DateTime endTime = DateTime.Now.Add(incrementInterval);
                DateTime nextIncrementTime = DateTime.Now.Add(rateIncrementInterval);

                bool isTestCompleted = false;

                while (DateTime.Now < endTime && !isTestCompleted)
                {
                    if (type == TestType.Smoke)
                    {
                        // Para Smoke test, realiza solo una petición y marca el test como completado
                        isTestCompleted = true;
                    }
                    else if (DateTime.Now >= nextIncrementTime)
                    {
                        if (type == TestType.Spike)
                        {
                            currentLoad += random.Next(100, 1001);
                        }
                        else
                        {
                            currentLoad += 50;
                        }
                        nextIncrementTime = DateTime.Now.Add(rateIncrementInterval);
                    }

                    //view.displayMessage($"Incremento: {currentLoad} peticiones lanzadas.");

                    var tasks = new List<Task>();
                    for (int i = 0; i < currentLoad; i++)
                    {
                        switch (switchOption)
                        {
                            case 1:
                                SendRequestAsync(type);
                                break;

                            case 2:
                                SendRequestAsyncById(type);
                                break;

                            case 3:
                                postRequestAsync(type);
                                break;

                            case 4:
                                PutRequestAsync(type);
                                break;

                            case 5:
                                DeleteRequestAsync(type, i);
                                break;


                            default:
                                throw new ArgumentException("Tipo de test no válido");
                        }

                        ;

                    }


                    //Console.WriteLine($"{DateTime.Now}: {tasks.Count} tareas ejecutadas.");
                    
                }

                Console.WriteLine("Test de carga completado.");
            }
            catch (Exception ex)
            {
                view.displayMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Send HTTP request to api by given url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task SendRequestAsync(TestType t)
        {

            sender.executeGetRequest(t);

        }

        /// <summary>
        /// Send HTTP request to api by given url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task SendRequestAsyncById(TestType t)
        {

            sender.executeGetRequestById(t, Guid.Parse("00527be4-14d3-4f4a-8daf-52b1a3ef3f55"));

        }



        /// <summary>
        /// Post method to create user
        /// </summary>
        /// <returns></returns>
        private async Task postRequestAsync(TestType type)
        {
            
            sender.executePostRequest(type, new ResponseObject(DateTime.Now.ToString(), DateTime.Now.ToString()));

        }
        private async Task PutRequestAsync(TestType type)
        {
            sender.executePutRequest(type, Guid.Parse("00527be4-14d3-4f4a-8daf-52b1a3ef3f55"));

        }
        private async Task DeleteRequestAsync(TestType type, int i)
        {
            //Guid id = Guid.Parse("fe20a2ca-f015-44f9-afce-ff6e8aa3b0fa");

            if (idsToDelete.Count>0)
            {
                sender.executeDeleteRequest(type, idsToDelete[i]);
            }


        }

        public static List<Guid> CreateUniqueNewGameIdList()
        {
            var uniqueNewGameIds = new List<Guid>();
            // Obtiene los IDs que están en newGameIds pero no en existingGameIds
            if (Controller.Controller.newGameIds.Count>0 && Controller.Controller.existingGameIds.Count>0)
            {
                uniqueNewGameIds = Controller.Controller.newGameIds.Except(Controller.Controller.existingGameIds).ToList();
                return uniqueNewGameIds;
            }

            return uniqueNewGameIds;
            
        }

    }
}
