# Klassbeskrivning



## GamePiece

| Variabler                                   | Beskrivning            |
| ------------------------------------------- | ---------------------- |
| public int PieceID                          | //id för spelpjäs      |
| public int CurrentPos                       | //lokal position       |
| public int CurrentGlobalPos                 | //global position      |
| public int LocalStartPos                    | //lokal start position |
| public int GoalPosIndex                     | //position för mål     |
| public bool PieceInGoal                     | //om spelpjäs är i mål |
| public IList<bool> LocalCoordinatePositions | //pjäsens spelbräde    |

| Metoder                                 | Beskrivning                                                  |
| --------------------------------------- | ------------------------------------------------------------ |
| private void InitializeLocalPositions() | //instantierar IList<bool> LocalCoordinatePositions med positioner |

**Klassbeskrivning:** En spelpjäs som används i klassen GamePlayer