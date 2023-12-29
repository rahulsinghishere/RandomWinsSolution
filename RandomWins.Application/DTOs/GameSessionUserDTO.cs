using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Application.DTOs
{
    public struct GameSessionUserRequestDTO
    {
        public string UserEmailAddress { get; set; }
        public int GameSessionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserFullName { get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            } }
    }

    public struct GameSessionUserResponseDTO
    {
        public GameSessionUserRequestDTO RequestDetails { get; set; }
        public int RandomNumber { get; set; }
        
    }

    public struct GameSessionWinner
    {
        public string WinnerFullName { get; set; }
        public string GameSessionName { get; set; }
        public int WinningNumber { get; set; }
        public string WinnerUserEmailAddress { get; set; }
        public int GameSessionId { get; set; }
    }
}

