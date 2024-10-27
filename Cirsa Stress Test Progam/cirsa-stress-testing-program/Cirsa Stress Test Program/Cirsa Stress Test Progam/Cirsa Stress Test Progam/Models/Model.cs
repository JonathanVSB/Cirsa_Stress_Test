namespace Cirsa_Stress_Test_Progam.Models
{
    public class Model
    {
        HttpClient httpClient;
        TestExecutor executor;
        RequestSender requestSender;
        public Model()
        {
            httpClient = new HttpClient();
            executor = new TestExecutor();
            requestSender = new RequestSender();
        }

        public async Task ExecuteSelectedTest(TestType type, int switchOption) 
        {
            await executor.ExecuteTestAsync(type, switchOption);
            
        }
        
    }
}
