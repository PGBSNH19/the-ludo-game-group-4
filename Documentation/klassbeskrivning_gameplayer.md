# Klassbeskrivning



## GamePlayer

| Variabler                      | Beskrivning                                  |
| ------------------------------ | -------------------------------------------- |
| public int GamePlayerID        | //id för spelare                             |
| public string Name             | //namn för spelare                           |
| public string Color            | //spelares färg                              |
| public int GlobalStartPos      | //en färgs startpos på gemensamma spelbrädet |
| public IList<GamePiece> Pieces | //lista innehållande spelpjäser              |

| Metoder                        | Beskrivning                                             |
| ------------------------------ | ------------------------------------------------------- |
| private void InitializePiece() | //Initierar listan med 4 pjäser då en GamePlayer skapas |

**Klassbeskrivning:** Används i klassen GameBoard. En ny Gameplayer instantieras för varje spelare och läggs i listan IList<GamePlayer> GamePlayers i GameBoard.