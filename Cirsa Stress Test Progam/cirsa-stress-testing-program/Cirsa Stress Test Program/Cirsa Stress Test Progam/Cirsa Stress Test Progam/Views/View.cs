namespace Cirsa_Stress_Test_Progam.Views
{
    public class View
    {

        /// <summary>
        /// Shows a message depending of boolean value
        /// </summary>
        /// <param name="success"></param>
        public string successStatus(bool success)
        {
            if (success)
            {
                string message = $"[{success}]: API is reachable and returned a successful response.\n";
               
                return message;
            }
            else
            {
                string message = $"[{success}]: Received a non-successful response.\n";
                
                return message;
            }
        } 

        /// <summary>
        /// Prints a message in console
        /// </summary>
        /// <param name="message"></param> message to print
        public void displayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
