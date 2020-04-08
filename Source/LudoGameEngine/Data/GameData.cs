using LudoGameEngine.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.Data
{
    public class GameData
    {
        DataContext Context = new DataContext();
        public int SessionId { get; set; }
        public int PlayerId { get; set; }
        public void InsertSessionData(string SessionName, bool finished = false, string winner = "")
        {
            try
            {
                var sName = Context.Session
                    .Where(x=> x.SessionName==SessionName)
                    .Select(x => x.SessionName).FirstOrDefault();

                if (sName!=SessionName)
                {
                    Session session = new Session
                    {
                        SessionName = SessionName,
                        GameFinished = finished,
                        Winner = winner,
                    };

                    Context.Session.Add(session);
                    Context.SaveChanges();
                    SessionId = session.SessionID;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"New Session [ {SessionName} ] Created Successfully\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("OBS, Session Creation Failed, Name is busy\n");
                    Console.ResetColor();
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
                Console.WriteLine("\nGiving Piece To Player, Please wait...\n");
                if (currentPlayer == 1)
                {
                    InsertPlayerData(name, color);
                    //this loop is used to give each player 4 pieces
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
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Data Inserting Failed\n");
                Console.ResetColor();
            }
        }

        public void InsertPlayerData(string name, string color)
        {
            try
            {
                Player player = new Player
                {
                    Name = name,
                };
                Context.Player.Add(player);
                Context.SaveChanges();

                PlayerSession ps = new PlayerSession
                {
                    PlayerId = player.PlayerID,
                    SessionId = SessionId,
                    Color = color
                };
                Context.PlayerSession.Add(ps);
                Context.SaveChanges();
                PlayerId = player.PlayerID;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Player could not save in database.}\nMake sure to create new game session \n");
                Console.ResetColor();
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
                    PlayerId = PlayerId,
                    PieceId = p.PieceID,
                };
                Context.PlayerPiece.Add(playerP);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Pieces could not save in database\n");
                Console.ResetColor();
            }
        }

        public void ShowHighScore()
        {
            try
            {
                var score = Context.Session
                     .Where(c => c.GameFinished == true)
                     .GroupBy(x => x.Winner)
                     .Select(w => new { winner = w.Key, Count = w.Count() })
                     .OrderByDescending(x => x.Count);

                foreach (var item in score)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Player Name {0} \t Wone {1}", item.winner, item.Count);
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, No data found \n");
                Console.ResetColor();
            }
        }

        public void ShowData()
        {
            try
            {
                var mySessionID = (from s in Context.Session
                                   orderby s.SessionID descending
                                   select s.SessionID).ToList().FirstOrDefault();

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
                Console.WriteLine("All players saved to database");
                foreach (var i in data)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Name : {i.name}\t Color: {i.color}\t Session Name: {i.session}");
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Failed to read data from database\n");
                Console.ResetColor();
            }
        }
        public void RemoveEverything()
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
                Console.WriteLine("OBS, data removed from database\n");
                Console.ResetColor();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, data could not be removed from database\n");
                Console.ResetColor();
            }
        }

        public void UpdatePiecePosition(string sessionName, string playerName, int pieceId, int position)
        {
            try
            {
                var data = (from pl in Context.Player
                            join ps in Context.PlayerSession
                            on pl.PlayerID equals ps.PlayerId
                            join s in Context.Session
                            on ps.SessionId equals s.SessionID
                            join pp in Context.PlayerPiece
                            on pl.PlayerID equals pp.PlayerId
                            join p in Context.Piece
                            on pp.PieceId equals p.PieceID
                            where pl.Name == playerName && s.SessionName == sessionName && p.PlayerPieceID == pieceId
                            select new
                            {
                                p.PlayerPieceID,
                                p.Position
                            }).FirstOrDefault();

                if (data.PlayerPieceID == pieceId)
                {
                    var Pposition = Context.Piece
                        .Where(x => x.PlayerPieceID == pieceId)
                        .FirstOrDefault();

                    Pposition.Position = position;
                    Context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Position Updated Successfully\n");
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, Position Could not updated\n");
                Console.ResetColor();
            }
        }

        public void UpdateWinner(string sessionName, bool gFinished, string winnerName)
        {
            try
            {
                var gameF = Context.Session
                       .Where(c => c.SessionName == sessionName)
                       .FirstOrDefault();

                if (gameF != null)
                {
                    gameF.GameFinished = gFinished;
                    Context.SaveChanges();
                }

                var player = (from s in Context.Session
                        join ps in Context.PlayerSession
                        on s.SessionID equals ps.SessionId
                        join p in Context.Player
                        on ps.PlayerId equals p.PlayerID
                        where s.SessionID == ps.SessionId && s.SessionName==sessionName && p.Name==winnerName
                        select new { s.Winner, p.Name}).FirstOrDefault();
               
                var setWinner = Context.Session
                    .Where(x => x.GameFinished == true)
                    .FirstOrDefault();

                if (player.Name !=null)
                {
                    setWinner.Winner = winnerName;
                    Context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Congratulation your wining {winnerName.ToUpper()}\n");
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, could not update data \n");
                Console.ResetColor();
            }
        }

        public class MyGameData
        {
            public string SessionName { get; set; }
            public string PlayerName { get; set; }
            public string Color { get; set; }
            public int PieceID { get; set; }
            public int Position { get; set; }
        }

        public List<MyGameData> LoadGame(string sName)
        {
            try
            {
                List<MyGameData> myData = new List<MyGameData>();
                var data = (from pl in Context.Player
                            join ps in Context.PlayerSession
                            on pl.PlayerID equals ps.PlayerId
                            join s in Context.Session
                            on ps.SessionId equals s.SessionID
                            join pp in Context.PlayerPiece
                            on pl.PlayerID equals pp.PlayerId
                            join p in Context.Piece
                            on pp.PieceId equals p.PieceID
                            where s.GameFinished == false && p.PlayerPieceID == 4 && s.SessionName==sName
                            select new
                            {
                                sessionName = s.SessionName,
                                playerName = pl.Name,
                                color = ps.Color,
                                piecePosition = p.Position,
                                pieceID = p.PlayerPieceID
                            }).ToList();

                if (data.Count()==0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("OBS, There is no unfinished game available \n");
                    Console.ResetColor();
                }
                else
                foreach (var item in data)
                {
                    MyGameData m = new MyGameData()
                    {
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS, There is no unfinished game available \n");
                Console.ResetColor();
                Console.ReadKey();
                throw;
            }
        }
    }
}
