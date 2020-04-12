using LudoGameEngine.Data;
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
        IGameSession SetSessionName();
        IGameSession SetPlayerAmount();
        IGameSession SetSessionData();
        IGameSession SaveState();
        IGameSession StartGame();
        int GetPlayerAmount();
        IList<Tuple<int, string, string>> GetSessionData();
    }
    public class GameSession: IGameSession
    {

        public int PlayerAmount { get; set; } = 0;
        public string PlayerName { get; set; } = "";
        public string SessionName { get; set; } = "";

        public IList<string> PlayerNames = new List<string>();
        public IList<string> Colors = new List<string>();

        public IList<Tuple<int, string, string>> SessionPlayerData = new List<Tuple<int, string, string>>();

        public bool IsPlayerNameValid()
        {
            return !string.IsNullOrWhiteSpace(PlayerName);
        }

        public IGameSession InintializeSession()
        {
            GameSession gameSession = new GameSession();
            return this;
        }

        public IGameSession SetSessionName()
        {
            Console.WriteLine("Please Enter a Session Name");
            Console.WriteLine("Input Sessioname: ");
            SessionName = Console.ReadLine();

            return this;
        }

        public IGameSession SetPlayerAmount()
        {
            Console.Clear();
            Console.WriteLine("How many players will play ?");

            string[] avaliablePlayers = {"[   2   ]", "[   3   ]", "[   4   ]"};
            PlayerAmount = (2 + CreateInteractable.OptionMenu(true, avaliablePlayers, 0, 2));

            return this;
        }
        public IGameSession SetSessionData()
        {
            Console.Clear();

            string[] tmpOptions = Enum.GetNames(typeof(GameColors));
            List<string> colorOptions = new List<string>(tmpOptions);

            Console.WriteLine("Please type in your names");

            for (int i = 1; i <= PlayerAmount; i++)
            {
                DrawGFX.SetDrawPosition(0, 1);
                Console.Write($"Name player {i}: ");
                PlayerName = Console.ReadLine();
                while (string.IsNullOrEmpty(PlayerName))
                {
                    Console.Clear();
                    Console.WriteLine("Sorry. You have to fill in a name");
                    Console.Write($"Name player {i}: ");
                    PlayerName = Console.ReadLine();                   
                }

                DrawGFX.SetDrawPosition(0, 3);
                Console.WriteLine("Choose your player color:");
                
                int colorID = CreateInteractable.OptionMenu(true, colorOptions, 0, 5);

                string choosenColor = colorOptions[colorID];
                SessionPlayerData.Add(Tuple.Create(i, PlayerName, choosenColor));
                
                colorOptions.RemoveAt(colorID);
                DrawGFX.ClearDrawContent(0, 3);
                DrawGFX.ClearDrawContent(0, 1);

                //d.InsertEachPlayerData(i, PlayerName, choosenColor); //inserting player data to DB
            }

            return this;
        }

        //Save SessionData to Database
        public IGameSession SaveState()
        {
            GameData d = new GameData();
            d.InsertSessionData(SessionName);  //creating new session, Name must be Unique

            foreach(var spdItem in SessionPlayerData)
            {                
                d.InsertEachPlayerData(spdItem.Item1, spdItem.Item2.ToLower(), spdItem.Item3); //inserting player data to DB
            }

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