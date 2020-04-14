using LudoGameEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LudoGameEngine.Data
{
    public class GameData
    {
        DataContext Context = new DataContext();
        public int SessionId { get; set; } //holding sessiond id for foreign key in PlayerSession table
        public int PlayerId { get; set; }   // holding player id for foreign key in playerSession table
        public int CheckSessionCreated { get; set; }
        //create new session and save it to database
        public void InsertSessionData(string SessionName, bool finished = false, string winner = "")
        {
            try
            {
                var sName = Context.Session  // finding an existing session name
                    .Where(x => x.SessionName == SessionName)
                    .Select(x => x.SessionName).FirstOrDefault();

                if (sName != SessionName)  //checking for existing name in database
                {
                    Session session = new Session
                    {
                        SessionName = SessionName.ToUpper(),
                        GameFinished = finished,
                        Winner = winner.ToUpper(),
                    };

                    Context.Session.Add(session);
                    Context.SaveChanges();
                    SessionId = session.SessionID;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"New Session [ {SessionName} ] Created Successfully\n");
                    Console.ResetColor();
                    Console.Beep(500, 200);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("OBS, Session Creation Failed, Name is busy\n");
                    Console.ResetColor();
                    Console.Beep(900, 900);
                    CheckSessionCreated = 1;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Session Could Not Created\n");
                Console.ResetColor();
            }
        }

        public void InsertEachPlayerData(int currentPlayer, string name, string color)
        {
            try
            {
                if (CheckSessionCreated == 0)  // check if new session has created
                {
                    Console.WriteLine("\nGiving Piece To Player, Please wait...\n");
                    if (currentPlayer == 1)
                    {
                        InsertPlayerData(name, color);
                        //giving each player 4 pieces
                        for (int j = 1; j <= 4; j++)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Giving Piece: {j}");
                            Console.ResetColor();
                            InsertPieceData(id: j, position: 0);
                        }
                    }
                    if (currentPlayer == 2)
                    {
                        InsertPlayerData(name, color);
                        for (int j = 1; j <= 4; j++)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Giving Piece: {j}");
                            Console.ResetColor();
                            InsertPieceData(id: j, position: 0);
                        }
                    }
                    if (currentPlayer == 3)
                    {
                        InsertPlayerData(name, color);
                        for (int j = 1; j <= 4; j++)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Giving Piece: {j}");
                            Console.ResetColor();
                            InsertPieceData(id: j, position: 0);
                        }
                    }
                    if (currentPlayer == 4)
                    {
                        InsertPlayerData(name, color);
                        for (int j = 1; j <= 4; j++)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Giving Piece: {j}");
                            Console.ResetColor();
                            InsertPieceData(id: j, position: 0);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please create a session\n");
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Data Inserting Failed\n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }

        public void InsertPlayerData(string name, string color)
        {
            try
            {
                Player player = new Player
                {
                    Name = name.ToUpper(),
                };
                Context.Player.Add(player);
                Context.SaveChanges();

                PlayerSession ps = new PlayerSession
                {
                    PlayerId = player.PlayerID, //giving foreign key same player ID
                    SessionId = SessionId,  //giving all players in same session same session id 
                    Color = color.ToUpper()
                };
                Context.PlayerSession.Add(ps);
                Context.SaveChanges();
                PlayerId = player.PlayerID;
                Console.Beep(500, 200);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Player could not save in database.}\nMake sure to create new game session \n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }

        public void InsertPieceData(int id, int position = 0)
        {
            try
            {
                Piece p = new Piece
                {
                    Position = position,
                    PlayerPieceID = id
                };
                Context.Piece.Add(p);
                Context.SaveChanges();

                PlayerPiece playerP = new PlayerPiece
                {
                    PlayerId = PlayerId, //giving same player id to forein key
                    PieceId = p.PieceID, // giving same piece id  to foreign key
                };
                Context.PlayerPiece.Add(playerP);
                Context.SaveChanges();
                Console.Beep(500, 200);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Pieces could not save in database\n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }

        public void ShowHighScore()
        {
            try
            {   //Group by winner and count their wins
                var score = Context.Session
                     .Where(c => c.GameFinished == true)
                     .GroupBy(x => x.Winner)
                     .Select(w => new { winner = w.Key, Count = w.Count() })
                     .OrderByDescending(x => x.Count);

                if (score != null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Data for high score players are ready\n");
                    Console.ResetColor();
                    Console.Beep(500, 200);
                }
                foreach (var item in score)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Player: {0} \t Won: {1}", item.winner.ToUpper(), item.Count);
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, No data found \n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }

        public void ShowData()  // shows the recent added player and session 
        {
            try
            {   // finding last session that created
                var mySessionID = (from s in Context.Session
                                   orderby s.SessionID descending
                                   select s.SessionID).ToList().FirstOrDefault();
                //joining player and playerSession and sesssion tables to show data from
                var data = (from d in Context.Player
                            join ps in Context.PlayerSession
                            on d.PlayerID equals ps.PlayerId
                            join s in Context.Session
                            on ps.SessionId equals s.SessionID
                            where ps.SessionId == mySessionID
                            orderby s.SessionID descending
                            select new
                            {
                                name = d.Name,
                                color = ps.Color,
                                session = s.SessionName
                            });

                if (data.ToList().Count > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Data that recently saved in database\n");
                    Console.ResetColor();
                    Console.Beep(500, 200);

                    foreach (var i in data)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Name : {i.name}\t Color: {i.color}\t Session Name: {i.session}");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nothing saved to database\n");
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Failed to read data from database\n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }
        public void RemoveEverything()   //remove everything from all tables, this method was created to clean all test data 
        {
            try
            {
                var player = Context.Player
                       .Where(p => p.PlayerID > 0);
                Context.RemoveRange(player);

                var session = Context.Session
                   .Where(p => p.SessionID > 0);
                Context.RemoveRange(session);

                var piece = Context.Piece
                   .Where(p => p.PieceID > 0);
                Context.RemoveRange(piece);
                Context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("OBS, All data removed from database\n");
                Console.ResetColor();
                Console.Beep(500, 200);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, data could not be removed from database\n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }

        public void UpdatePiecePosition(string sessionName, string playerName, int pieceId, int position)
        {
            try
            {
                var pos = (from pl in Context.Player
                          join pp in Context.PlayerPiece
                          on pl.PlayerID equals pp.PlayerId
                          join p in Context.Piece
                          on pp.PieceId equals p.PieceID
                          join ps in Context.PlayerSession
                          on pl.PlayerID equals ps.PlayerId
                          join s in Context.Session
                          on ps.SessionId equals s.SessionID
                          where pl.Name == playerName && pp.PlayerId == pl.PlayerID && p.PlayerPieceID== pieceId && s.SessionName == sessionName
                           select p.PieceID).FirstOrDefault();

                if (pos!=0)
                {
                    var pPosition = Context.Piece
                        .Where(x => x.PieceID == pos)
                        .FirstOrDefault();

                    pPosition.Position = position;
                    Context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Position Updated Successfully\n");
                    Console.ResetColor();
                    Console.Beep(500, 150);
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Position Could not updated\n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }

        public void UpdateWinner(string sessionName, bool gFinished, string winnerName)
        {
            try
            {   //Finding the Session name that user want
                var gameF = Context.Session
                       .Where(c => c.SessionName == sessionName.ToUpper())
                       .FirstOrDefault();

                if (gameF != null)  //update gameFinished row in session Table
                {
                    gameF.GameFinished = gFinished;
                    Context.SaveChanges();
                }

                // finding the player that we want in the session
                var player = (from s in Context.Session
                              join ps in Context.PlayerSession
                              on s.SessionID equals ps.SessionId
                              join p in Context.Player
                              on ps.PlayerId equals p.PlayerID
                              where s.SessionID == ps.SessionId && s.SessionName == sessionName.ToUpper() && p.Name == winnerName.ToUpper()
                              select new { s.Winner, p.Name }).FirstOrDefault();

                var setWinner = Context.Session  //checking if Game has finished
                    .Where(x => x.GameFinished == true && sessionName.ToUpper()== x.SessionName)
                    .FirstOrDefault();

                if (player.Name != null)
                {
                    setWinner.Winner = winnerName.ToUpper(); //updating winner only if game has finished
                    Context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Congratulation your wining {winnerName.ToUpper()}\nData successfully saved in database");
                    Console.ResetColor();
                    Console.Beep(500, 200);
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, could not update data \n");
                Console.ResetColor();
                Console.Beep(900, 900);
            }
        }

        public List<string> ShowAllSession()
        {
            try
            {    //show all session name that did not finished 
                var session = Context.Session
                        .Where(x => x.GameFinished == false)
                        .Select(x => x.SessionName).ToList();

                return session;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, No session available for playing \n");
                Console.ResetColor();
                Console.Beep(900, 900);
                Console.ReadKey();
                throw;
            }
        }
        public class MyGameData
        {
            public int PlayerID { get; set; }
            public string SessionName { get; set; }
            public string PlayerName { get; set; }
            public string Color { get; set; }
            public int PieceID { get; set; }
            public int Position { get; set; }
        }

        public List<MyGameData> LoadGame(string sName)
        {
            try
            {   //creating a list of object
                List<MyGameData> myData = new List<MyGameData>();

                //joining all tables to getting game data that are available for playing
                var data = (from pl in Context.Player
                            join ps in Context.PlayerSession
                            on pl.PlayerID equals ps.PlayerId
                            join s in Context.Session
                            on ps.SessionId equals s.SessionID
                            join pp in Context.PlayerPiece
                            on pl.PlayerID equals pp.PlayerId
                            join p in Context.Piece
                            on pp.PieceId equals p.PieceID
                            where s.GameFinished == false && s.SessionName == sName
                            select new
                            {
                                playerId = pl.PlayerID,
                                sessionName = s.SessionName,
                                playerName = pl.Name,
                                color = ps.Color,
                                piecePosition = p.Position,
                                pieceID = p.PlayerPieceID
                            }).ToList();

                if (data.Count() == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("OBS, There is no unfinished game available \n");
                    Console.ResetColor();
                    Console.Beep(900, 900);
                }
                else
                    foreach (var item in data)
                    {
                        MyGameData m = new MyGameData()
                        {
                            PlayerID = item.playerId,
                            SessionName = item.sessionName,
                            PlayerName = item.playerName,
                            Color = item.color,
                            PieceID = item.pieceID,
                            Position = item.piecePosition
                        };
                        myData.Add(m);
                    }
                return myData;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
