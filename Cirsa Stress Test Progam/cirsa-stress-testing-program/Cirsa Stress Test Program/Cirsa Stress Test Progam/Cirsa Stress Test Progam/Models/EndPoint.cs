namespace Cirsa_Stress_Test_Progam.Models
{
    public class EndPoint
    {
        private static string port = "5212";
        private static string ip = "localhost";
        private static string url = $"http://{ip}:{port}/";

        // Get game data endpoint
        public static string GetGameData => url + "gamedata";
        

        // Get game data by Id endpoint
        public static string GetGameDataById(string id)
        {
            return url + "gamedata/" + id;
        }
    }

}
