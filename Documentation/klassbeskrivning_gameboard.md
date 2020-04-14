# Klassbeskrivning



## GameBoard

| Variabler                                             | Beskrivning |
| ----------------------------------------------------- | ----------- |
| public IList<BoardCoordinate> CoordinateOuterPosition |             |
| public IList<GamePlayer> GamePlayers                  |             |
| public int GamePlayerAmnt                             |             |
| public string SessionName                             |             |
| public bool NewGame                                   |             |
| public Dice dice                                      |             |
| private string winner                                 |             |
| private int saveGame                                  |             |
| private ConsoleColor playerTextColor                  |             |
| private IGameSession gs                               |             |
| private GameData gameData                             |             |
| private int gfxInfoPos                                |             |
| private int gfxSubInfoPos                             |             |
| private int gfxStatusPos                              |             |
| private int gfxResultPos                              |             |
| private int gfxInteractableInfoPos                    |             |
| private int gfxInteractablePos                        |             |
| private int gfxDividerPos                             |             |
| private int gfxGameBoardTitlePos                      |             |
| private int gfxGameBoardPiecePos                      |             |
| private int gfxGameBoardPos                           |             |
| private int gfxPlayerBoardTitlePos                    |             |
| private int gfxPlayerInfoPos                          |             |
| private int gfxPieceBoard1Pos                         |             |
| private int gfxPieceBoard2Pos                         |             |
| private int gfxPieceBoard3Pos                         |             |
| private int gfxPieceBoard4Pos                         |             |

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

