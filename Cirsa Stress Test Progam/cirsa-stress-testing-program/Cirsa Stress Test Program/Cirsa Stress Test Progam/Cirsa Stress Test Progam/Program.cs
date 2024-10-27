using Cirsa_Stress_Test_Progam.Models;
namespace Cirsa_Stress_Test_Progam
{
    internal class Program
    {
        /**
            * @author $Jonathan Segura - 
            *
            * @date - $21/02/2024$
        */
        static async Task Main(string[] args)
        {
            Model model = new Model();

            Controller.Controller controller = new Controller.Controller(model);

            await controller.initTest();
        }
    }
}