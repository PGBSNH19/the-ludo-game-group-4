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

            DrawGFX.SetDrawPosition(0, 0);
            Console.WriteLine("Welcome to LudoFrenzy".ToUpper());

            int selected = CreateInteractable.OptionMenu(false, options, 0, 2);

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
            Console.WriteLine("Load Game");
            List<string> sessionOption = Data.ShowAllSession();
            int loadOption = CreateInteractable.OptionMenu(false, sessionOption, 0, 2);
            string sessionName = sessionOption[loadOption];
            var SessionData = Data.LoadGame(sessionName);

            GameBoard gb = new GameBoard(new GameSession(), false);
            List<string> playerAmount = new List<string>();
            int index = 0;
            foreach (var i in SessionData)
            {
                playerAmount.Add(i.PlayerName);
                gb.GamePlayers.Add(new GamePlayer(id: i.PlayerID,name: i.PlayerName, color:i.Color ));


                //for (int j = 0; j <= 4; j++)
                //{
                //    gb.GamePlayers[index].Pieces[j].CurrentPos = i.Position;
                //}


                //if (index==0)
                //{
                //    for (int j = 1; j <= 4; j++)
                //    {
                //        gb.GamePlayers[index].Pieces.Add(new GamePiece(id: j) {CurrentPos=i.Position });
                //    }
                //}
                //else if (index == 1)
                //{
                //    for (int j = 1; j <= 4; j++)
                //    {
                //        gb.GamePlayers[index].Pieces.Add(new GamePiece(id: j) { CurrentPos = i.Position });
                //    }
                //}


                index++;
            }
            gb.GamePlayerAmnt = playerAmount.Count;
            gb.GameLoop();


            //Console.WriteLine("Load");
            //Console.WriteLine("Please Enter the session name");
            //string sessionName = Console.ReadLine();
            //string sessionN = "";


            //int index = 0;
            //foreach (var i in Data.LoadGame(sessionName))
            //{
            //    playerAmount.Add(i.PlayerName);
            //    sessionN = i.SessionName;

            //    game.GamePlayers[index].GamePlayerID = 0;
            //    game.GamePlayers[index].Name = i.PlayerName;
            //    game.GamePlayers[index].Color = i.Color;
            //    game.GamePlayers[index].GlobalStartPos = i.Position;


            //    Console.WriteLine($"Player ID: {i.PlayerID} Session Name: {i.SessionName}\tPlayerName: {i.PlayerName}\t" +
            //        $"Color: {i.Color}\tPiece ID: {i.PieceID}\tPosition {i.Position}");
            //    index++;
            //}

            /*game.gamePlayerAmnt = playerAmount.Count;
            game.SessionName = sessionN;
            game.GameLoop();*/


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
