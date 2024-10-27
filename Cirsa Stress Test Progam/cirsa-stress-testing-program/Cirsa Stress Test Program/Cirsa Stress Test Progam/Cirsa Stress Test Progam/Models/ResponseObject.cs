using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cirsa_Stress_Test_Progam.Models
{
    public class ResponseObject
    {

        public Guid Id { get; set; }
        public string GameName { get; set; }
        public string Category { get; set; }
        public int TotalBets { get; set; }
        public int TotalWins { get; set; }
        public float AverageBetAmount { get; set; }
        public float PopularityScore { get; set; }
        public DateTime LastUpdated { get; set; }

        public ResponseObject() { }
        public ResponseObject(string gameName, string category)
        {
            GameName = gameName;
            Category = category;
        }

        public ResponseObject(Guid id)
        {
            this.Id = id;
        }

        public ResponseObject(string gameName, string category, float popularityScore, int totalBets, int totalWins, float averageBetAmount) : this(gameName, category)
        {
            PopularityScore = popularityScore;
            TotalBets = totalBets;
            TotalWins = totalWins;
            AverageBetAmount = averageBetAmount;
        }

        public ResponseObject(Guid id, string gameName, string category, int totalBets, int totalWins, float averageBetAmount, float popularityScore, DateTime lastUpdated) : this(id)
        {
            Id = id;
            GameName = gameName;
            Category = category;
            TotalBets = totalBets;
            TotalWins = totalWins;
            AverageBetAmount = averageBetAmount;
            PopularityScore = popularityScore;
            LastUpdated = lastUpdated;
        }
    }
}
