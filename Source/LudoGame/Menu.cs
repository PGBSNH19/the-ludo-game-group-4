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
                gb.GameLoop();

            }


            //int index = 0;
            //foreach (var i in playerData)
            //{
            //    playerAmount.Add(i[0].PlayerName);
            //    gb.GamePlayers.Add(new GamePlayer(id: i[0].PlayerID, name: i[0].PlayerName, color: i[0].Color));

            //    for (int j = 0; j <= 4; j++)
            //    {
            //        gb.GamePlayers[index].Pieces[j].CurrentPos = SessionData[j].Position;
            //    }
            //    index++;
            //}





            //int index = 1;
            //foreach (var e in SessionData)
            //{
            //    gb.GamePlayers[index].Pieces.Add(new GamePiece(id: e.PieceID) { CurrentPos = e.Position });
            //    gb.GamePlayers.p
            //    index++;
            //}


            /*

                        //foreach (var i in SessionData)
                        //{
                        //    playerAmount.Add(i.PlayerName);
                        //    gb.GamePlayers.Add(new GamePlayer(id: i.PlayerID, name: i.PlayerName, color: i.Color));

                        //    GameData.MyGameData d = new GameData.MyGameData {PlayerID=i.PlayerID, PlayerName=i.PlayerName,Color=i.Color };
                        //    data.Add(d);
                        //}

                        foreach (var e in data)
                        {
                            Console.WriteLine("Player ID: {0} PlayerName: {1} Color: {2}", e.PlayerID, e.PlayerName,e.Color);



                            //var distinct = e.PlayerName.Distinct().ToList();
                            //foreach (var item in distinct)
                            //{
                            //    Console.WriteLine("Distinc" + item);
                            //}

                        }


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


                           // index++;
                       // }
                        gb.GamePlayerAmnt = playerAmount.Count;
                        //gb.GameLoop();


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
