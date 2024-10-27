using Cirsa_Stress_Test_Progam.Models;
using Cirsa_Stress_Test_Progam.Views;

namespace Cirsa_Stress_Test_Progam.Controller
{
    public class Controller
    {
        private Model model;
        private View view;
        ResponseObject obj;
        public static List<Guid> existingGameIds = new List<Guid>();
        public static List<Guid> newGameIds = new List<Guid>();
        TestExecutor tester;
        public Controller(Model model)
        {
            this.model = model;
            view = new View();
            tester = new TestExecutor();

        }
        public async Task initTest()
        {
            //Smoke Tests

            view.displayMessage("-----------STARTING SPIKE TEST--------------");

            await model.ExecuteSelectedTest(TestType.Smoke, 1);
            await model.ExecuteSelectedTest(TestType.Smoke, 2);
            await model.ExecuteSelectedTest(TestType.Smoke, 3);
            await model.ExecuteSelectedTest(TestType.Smoke, 4);
            await model.ExecuteSelectedTest(TestType.Smoke, 5);



            //Average Tests

            //view.displayMessage("-----------STARTING AVERAGE TEST--------------");

            await model.ExecuteSelectedTest(TestType.Average, 1);
            await model.ExecuteSelectedTest(TestType.Average, 2);
            await model.ExecuteSelectedTest(TestType.Average, 3);
            await model.ExecuteSelectedTest(TestType.Average, 4);
            await model.ExecuteSelectedTest(TestType.Average, 5);


            ////Spike Tests


            view.displayMessage("-----------STARTING SPIKE TEST--------------");

            await model.ExecuteSelectedTest(TestType.Spike, 1);
            await model.ExecuteSelectedTest(TestType.Spike, 2);
            await model.ExecuteSelectedTest(TestType.Spike, 3);
            await model.ExecuteSelectedTest(TestType.Spike, 4);
            await model.ExecuteSelectedTest(TestType.Spike, 5);

            runPDFMaker(PDFMaker.testSmokeMetrics);
            runPDFMaker(PDFMaker.testAverageMetrics);
            runPDFMaker(PDFMaker.testSpikeMetrics);
        }

        /// <summary>
        /// Create PDF using the data of the metrics obtained
        /// </summary>
        /// <param name="metricsList"></param>
        public void runPDFMaker(List<Metrics> metricsList)
        {
            PDFMaker pdfMaker = new PDFMaker();
            pdfMaker.generateLog(metricsList);

        }

    }
}
