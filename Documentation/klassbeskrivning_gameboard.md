# Klassbeskrivning



## GameBoard

| Variabler                                                    | Beskrivning                                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| public IList<BoardCoordinate> CoordinateOuterPosition        | //det gemensama spelarbrädet                                 |
| public IList<GamePlayer> GamePlayers                         | //lista där de GamePlayers som ingår i sessionen läggs in.   |
| public int GamePlayerAmnt                                    | //antal spelare som ingår i sessionen                        |
| public string SessionName                                    | //sessionsnamn                                               |
| public bool NewGame                                          | //om det är en ny session som skapas eller om man laddat ett sparat spel |
| public Dice dice                                             | //instans av klassen Dice                                    |
| private string winner                                        | //sätts då någon vinner spelomgången                         |
| private int saveOrSkip                                       | //returvärde för menyval spara eller skippa att spara        |
| private ConsoleColor playerTextColor                         | //hjälper till att skriva text i konsolen i samma färg som spelaren |
| private IGameSession gs                                      | //tar emot parametern i klassens konstruktor                 |
| private GameData gameData                                    | //instans av GameData som innehåller databasrelaterade metoder att använda i programmet |
|                                                              |                                                              |
| //positionsvariablar för att skriva text till olika positioner på consolen | //positionsvariablar för att skriva text till olika positioner på consolen. Används tillsammans med DrawGFX.SetPosition(pos x, pos y) |
| private int gfxInfoPos                                       |                                                              |
| private int gfxSubInfoPos                                    |                                                              |
| private int gfxStatusPos                                     |                                                              |
| private int gfxResultPos                                     |                                                              |
| private int gfxInteractableInfoPos                           |                                                              |
| private int gfxInteractablePos                               |                                                              |
| private int gfxDividerPos                                    |                                                              |
| private int gfxGameBoardTitlePos                             |                                                              |
| private int gfxGameBoardPiecePos                             |                                                              |
| private int gfxGameBoardPos                                  |                                                              |
| private int gfxPlayerBoardTitlePos                           |                                                              |
| private int gfxPlayerInfoPos                                 |                                                              |
| private int gfxPieceBoard1Pos                                |                                                              |
| private int gfxPieceBoard2Pos                                |                                                              |
| private int gfxPieceBoard3Pos                                |                                                              |
| private int gfxPieceBoard4Pos                                |                                                              |

| Metoder                                                | Beskrivning |
| ------------------------------------------------------ | ----------- |
| public void GameLoop()                                 |             |
| private void SaveGame()                                |             |
| private void SaveWinner()                              |             |
| private void DisplayWinner()                           |             |
| private IList<string> CreatePieceBtnOptions()          |             |
| private IList<string> CreatePieceButtonOptionsInNest() |             |
| private int GetPlayerIndex()                           |             |
| private int GetPieceIndex()                            |             |
| private GamePiece GetPieceByID()                       |             |
| private int GetGlobalPosition()                        |             |
| private int GetPreviousPieceLocalPosition()            |             |
| private int GetNewLocalPiecePosition()                 |             |
| private void MoveLocalPiece()                          |             |
| private void MoveGlobalPiece()                         |             |
| private int CheckCollision()                           |             |
| public void InitializeGame()                           |             |
| private void InitializeBoardCoordinates()              |             |
| private int DecidePlayerStart()                        |             |
| public IList<GamePlayer> SetPlayOrder()                |             |
| public int SetColorStartPositon()                      |             |

