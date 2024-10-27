using Cirsa_Stress_Test_Progam.Views;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;

namespace Cirsa_Stress_Test_Progam.Models
{
    public class PDFMaker
    {
        public static List<Metrics> testSmokeMetrics = new List<Metrics>();
        public static List<Metrics> testAverageMetrics = new List<Metrics>();
        public static List<Metrics> testSpikeMetrics = new List<Metrics>();
        View view;

        public PDFMaker()
        {
            view = new View();
        }


        public void generateLog(List<Metrics> data, string pathfile = "testLogs.pdf")
        {
            try
            {
                data.AddRange(GetAverageMetrics(testAverageMetrics));
                data.AddRange(GetAverageMetrics(testSpikeMetrics));

                if (data != null)
                {
                    var documentBuilder = DocumentBuilder.New();
                    var sectionBuilder = documentBuilder.AddSection();
                    sectionBuilder.AddParagraph("Testing Logs").SetAlignment(HorizontalAlignment.Center).SetFontSize(24).SetBold();

                    foreach (Metrics metric in data)
                    {
                        sectionBuilder.AddParagraph(
                            metric.testName +
                            "Test:  (" + metric.time +
                            " seconds) [REQUEST: "+metric.type+"] [Response code: " + metric.code +
                            "] " + metric.description
                            ).SetBold();
                    }
                    documentBuilder.Build(pathfile);
                    view.displayMessage("PDF was created at" +pathfile);
                }
            }
            catch (Exception ex)
            {
                view.displayMessage("PDF was not created." + ex.Message);
            }
        }

        public static List<Metrics> GetAverageMetrics(List<Metrics> list)
        {
            Dictionary<string, double> totalTime = new Dictionary<string, double>();
            Dictionary<string, int> count = new Dictionary<string, int>();

            List<string> data = new List<string>() { "GET", "POST", "PUT", "DELETE" };

            foreach (var type in data)
            {
                totalTime[type] = 0;
                count[type] = 0;

                foreach (var metric in list)
                {
                    if (metric.type.Equals(type))
                    {
                        totalTime[type] += metric.time;
                        count[type]++;
                    }
                }
            }

            List<Metrics> averages = new List<Metrics>();

            foreach (var type in data)
            {
                double averageTime = count[type] > 0 ? totalTime[type] / count[type] : 0;
                Metrics average = new Metrics(list[0].testName, averageTime, list[0].description, type, list[0].code);
                averages.Add(average);
            }

            return averages;
        }

    }





}


