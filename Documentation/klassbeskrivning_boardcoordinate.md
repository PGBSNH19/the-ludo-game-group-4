# Klassbeskrivning



## BoardCoordinate

| Variabler                        | Beskrivning                          |
| -------------------------------- | ------------------------------------ |
| public bool IsOccupied           | //om rutan är ockuperad              |
| public int BoardPosition         | //position                           |
| public int OccupiedPlayerID      | //ID på spelare som ockuperar rutan  |
| public int OccupiedPlayerPieceID | //ID på spelpjäs som ockuperar rutan |

**Klassbeskrivning: **Hjälpklass till GameBoard när den gemensamma spelarplanen skapas (public IList<BoardCoordinate> CoordinateOuterPosition)