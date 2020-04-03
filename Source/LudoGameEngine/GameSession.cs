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
        IGameSession SetPlayerName();
        IGameSession GetPlayerProfile();
        IGameSession ChoosePlayerColor();
        IGameSession SetPlayerPositions();
        IGameSession SaveState();
        IGameSession StartGame();
        int GetPlayerAmount();
        IList<string> GetPlayerNames();
    }
    public class GameSession: IGameSession
    {
      
        public int PlayerAmount { get; set; }
        public string PlayerName { get; set; }
        public IList<string> PlayerNames = new List<string>();
        public IList<string> Colors = new List<string>();

        public enum Color
        {
            Red,
            Blue,
            Green,
            Yellow
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

            return this;
        }
        public IGameSession SetPlayerName()
        {
            Console.WriteLine("Please type in your names");

            for(int i = 1; i <= PlayerAmount; i++)
            {
                Console.Write($"Name player {i}: ");
                PlayerName = Console.ReadLine();
                PlayerNames.Add(PlayerName);
            }
            return this;
        }
        public IGameSession GetPlayerProfile() 
        {
            //get from database, check names if exists, else create new
            return this;
        }
        public IGameSession ChoosePlayerColor()
        {
            return this;
        }
        public IGameSession SetPlayerPositions()
        {
            return this;
        }
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

        public IList<string> GetPlayerNames()
        {
            return PlayerNames;
        }
    }
}