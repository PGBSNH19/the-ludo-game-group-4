using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            "HIGHSCORE",
            "LOAD GAME",
            "QUIT"
        };

        public static GameData Data = new GameData();
        public static void Display()
        {
            DrawGFX.SetDrawPosition(0, 0);
            Console.WriteLine("Welcome to LudoFrenzy".ToUpper());

            int selected = CreateInteractable.OptionMenu(false, options, 0, 2);

            switch (selected)
            {
                case 0:
                    IGameSession gs = new GameSession().
                         InintializeSession().
                         SetSessionName().
                         SetPlayerAmount().
                         SetSessionData().
                         SaveState().
                         StartGame();

                    GameBoard gb = new GameBoard(gs, true);
                    gb.GameLoop();

                    break;
                case 1:
                    DisplayHighScore();
                    break;
                case 2:
                    LoadSavedGames();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        static void DisplayHighScore()
        {
            Console.WriteLine("HighScore ");
            Data.ShowHighScore();
            Dictionary<string, int> topPlayers = new Dictionary<string, int>();


            BackButton();
            ReturnToMenu();
        }
        static void LoadSavedGames()
        {
            DrawGFX.SetDrawPosition(0, 1);
            Console.WriteLine("Load Game");
            if (Data.ShowAllSession().Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, There is no game session available \n");
                Console.ResetColor();
            }
            else
            {
                List<string> sessionOption = Data.ShowAllSession();
                int loadOption = CreateInteractable.OptionMenu(false, sessionOption, 0, 2);
                string sessionName = sessionOption[loadOption];
                var SessionData = Data.LoadGame(sessionName);

                GameBoard gb = new GameBoard(new GameSession(), false);
                gb.SessionName = sessionName;

                List<string> playerAmount = new List<string>();

                List<GameData.MyGameData> data = SessionData.Distinct().ToList();

                var playerData = SessionData
                    .GroupBy(x => new { x.PlayerName, x.PlayerID, x.Color })
                    .Select(x => x.ToList()).ToList();

                for (int i = 0; i < playerData.Count; i++)
                {
                    Console.WriteLine("ID: {0} Name: {1} Color: {2}", playerData[i][i].PlayerID, playerData[i][i].PlayerName, playerData[i][i].Color);
                    playerAmount.Add(playerData[i][i].PlayerName);
                    gb.GamePlayers.Add(new GamePlayer(id: playerData[i][i].PlayerID, name: playerData[i][i].PlayerName, color: playerData[i][i].Color));

                    for (int j = 0; j < 4; j++)
                    {
                        gb.GamePlayers[i].Pieces[j].CurrentPos = SessionData[j].Position;
                        Console.WriteLine("Piece ID: {0} Position: {1} ", SessionData[j].PieceID, SessionData[j].Position);
                    }
                }
                gb.GamePlayerAmnt = playerAmount.Count();
                gb.GameLoop();
            }
       
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
