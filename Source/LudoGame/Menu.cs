using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LudoGameEngine;
using LudoGameEngine.Data;

namespace LudoGame
{
    class Menu
    {
        private static readonly string[] options = 
        {
            "NEW GAME",
            "CONTINUE (Last saved game)",
            "HIGHSCORE",
            "LOAD GAME",
            "QUIT"
        };

        private static IDictionary<string, int> playerProfiles = new Dictionary<string, int>();
        public static GameData Data = new GameData();
        public static void Display()
        {
            playerProfiles = LoadHighScoreAsync().Result;

            int selected = CreateInteractable.OptionMenu(false, options, 0, 2, 0, 0, "Welcome to LudoFrenzy".ToUpper());

            switch (selected)
            {
                case 0:
                    IGameSession gs = new GameSession().
                         InintializeSession().
                         SetPlayerAmount().
                         SetSessionData().
                         //.GetPlayerProfile().
                         //ChoosePlayerColor().
                         //SetPlayerPositions().
                         SaveState().
                         StartGame();

                    GameBoard gb = new GameBoard(gs, true);
                    gb.GameLoop();

                    break;
                case 1:
                    ContinueLastSavedGame();
                    break;
                case 2:
                    DisplayHighScore(playerProfiles);
                    break;
                case 3:
                    LoadSavedGames();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
      
        static void ContinueLastSavedGame()
        {
            Console.WriteLine("Continue last saved game");
            ReturnToMenu();
        }

        static async Task<IDictionary<string, int>> LoadHighScoreAsync()
        {
            IDictionary<string, int> tmpProfiles = new Dictionary<string, int>();
            //tmpProfiles = await database.GetPlayerProfiles();

            return tmpProfiles;
        }

        static void DisplayHighScore(IDictionary<string, int> dict)
        {
            Console.WriteLine("HighScore ");
            Data.ShowHighScore();
            Dictionary<string, int> topPlayers = new Dictionary<string, int>();


            BackButton();
            ReturnToMenu();
        }
        static void LoadSavedGames()
        {
            //Console.WriteLine("Load");
            Console.WriteLine("Please Enter the session name");
            string sessioName = Console.ReadLine();
            List<string> playerAmount = new List<string>();

            foreach (var i in Data.LoadGame(sessioName))
            {
                playerAmount.Add(i.PlayerName);
                Console.WriteLine($"Session Name: {i.SessionName}\tPlayerName: {i.PlayerName}\tColor: {i.Color}\tPiece ID: {i.PieceID}\tPosition {i.Position}");
            }

              GameBoard game = new GameBoard(new GameSession(), false);
              game.gamePlayerAmnt = playerAmount.Count;
              game.GameLoop();
            BackButton();
            ReturnToMenu();
        }

        static void BackButton()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\r\n\rBACK");
            Console.ResetColor();
        }
        static void ReturnToMenu()
        {
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Display();
            }
        }
    }
}
