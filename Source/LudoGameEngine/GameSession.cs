using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    interface IGameSession
    {
        IGameSession InintializeSession();
        IGameSession SetPlayerAmount();
        IGameSession SetPlayerName();
        IGameSession GetPlayerProfile();
        IGameSession ChoosePlayerColor();
        IGameSession SetPlayerPositions();
        IGameSession SaveState();
        IGameSession StartGame();
    }
    public class GameSession: IGameSession
    {
        public int PlayerAmount { get; set; }
        public string PlayerName { get; set; }
        public IList<string> PlayerNames = new List<string>();
        public IList<string> Color = new List<string>();

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
            return this;
        }
        public  IGameSession StartGame()
        {
            return this;
        }
    }
}