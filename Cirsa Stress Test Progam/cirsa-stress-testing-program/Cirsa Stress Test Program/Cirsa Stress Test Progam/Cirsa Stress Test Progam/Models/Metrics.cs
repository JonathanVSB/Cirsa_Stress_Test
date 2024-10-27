namespace Cirsa_Stress_Test_Progam.Models
{
    public class Metrics
    {
        public string testName { get; set; }
        public double time { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int code { get; set; }

        
        public Metrics(string testName, double time, string description, string type, int code)
        {
            this.testName = testName;
            this.time = time;
            this.description = description;
            this.type = type;
            this.code = code;
        }

    }
}
