using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LudoGameEngine
{
    public interface IGameSession
    {
        IGameSession InintializeSession();
        IGameSession SetPlayerAmount();
        IGameSession SetSessionData();
        //IGameSession GetPlayerProfile();
       // IGameSession ChoosePlayerColor();
        //IGameSession SetPlayerPositions();
        IGameSession SaveState();
        IGameSession StartGame();
        int GetPlayerAmount();
        IList<Tuple<int, string, string>> GetSessionData();
    }
    public class GameSession: IGameSession
    {
      
        public int PlayerAmount { get; set; }
        public string PlayerName { get; set; }
        public IList<string> PlayerNames = new List<string>();
        public IList<string> Colors = new List<string>();

        public IList<Tuple<int, string, string>> SessionPlayerData = new List<Tuple<int, string, string>>();

        public enum Color
        {
            Red,
            Blue,
            Green,
            Yellow
        }

        public bool IsPlayerNameValid()
        {
            return !string.IsNullOrWhiteSpace(PlayerName);
        }

        public IGameSession InintializeSession()
        {
            GameSession gameSession = new GameSession();
            return this;
        }

        public IGameSession SetPlayerAmount()
        {
            Console.Write("How many players will play?: ");
            PlayerAmount = int.Parse(Console.ReadLine());
            while (PlayerAmount <2 || PlayerAmount > 4)
            {
                Console.WriteLine("Sorry. You have to be at least two and maximum four players to play");
                Console.Write("How many players will play?: ");
                PlayerAmount = int.Parse(Console.ReadLine());
            }
            
            return this;
        }
        public IGameSession SetSessionData()
        {
            Console.WriteLine("Please type in your names");

            for(int i = 1; i <= PlayerAmount; i++)
            {
                Console.Write($"Name player {i}: ");
                PlayerName = Console.ReadLine();

                //ersätt detta med menyoptions som med piltagenter. Måste skapa en ui-utilityclass
                Console.WriteLine("Choose Color: Red, Blue, Green, Yellow");
                string color = Console.ReadLine();

                SessionPlayerData.Add(Tuple.Create(i, PlayerName, color));
            }

            return this;
        }
        ///*public IGameSession GetPlayerProfile() 
        //{
        //    //get from database, check names if exists, else create new
        //    return this;
        //}
        //public IGameSession ChoosePlayerColor()
        //{

        //    return this;
        //}
        //public IGameSession SetPlayerPositions()
        //{
        //    return this;
        //}
        public IGameSession SaveState()
        {
            //Save initial to database
            return this;
        }
        public  IGameSession StartGame()
        {
            //call a new board
            return this;
        }

        public int GetPlayerAmount()
        {
            return PlayerAmount;
        }

        public IList<Tuple<int, string, string>> GetSessionData()
        {
            
            return SessionPlayerData;
        }
    }
}