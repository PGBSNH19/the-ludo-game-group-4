# Klassbeskrivning



## DrawGFX

| Variabler                                | Beskrivning                                                  |
| ---------------------------------------- | ------------------------------------------------------------ |
| public enum BoardGFXItem                 | //enkapsulerar vad för grafik som man får lägga in som parameter när man skapar en boarditem |
| public enum PieceGFXItem                 | //enkapsulerar vad för grafik som man får lägga in som parameter när man skapar en boarditem |
| public enum GameColors                   | //enkapsulerar vad för grafik som man får lägga in som parameter när man skapar en boarditem |
| public static string GameBoardItem       | //grafik för GameBoard                                       |
| public static string PieceBoardItem      | //grafik för PieceBoard                                      |
| public static string GameBoardPiecesItem | //grafik för spelarpjäser                                    |
| public static string PieceOnBoard        | //grafik för spelarpjäser på GameBoard                       |
| public static string PieceInNest         | //grafik för spelarpjäsen då den befinner sig i sitt bo      |

| Metoder                                               | Beskrivning                               |
| ----------------------------------------------------- | ----------------------------------------- |
| public static void SetDrawPosition()                  | //bestämmer ritposition på consolfönstret |
| public static void ClearDrawContent()                 | //rensar i consolfönstret Overload: rad   |
| public static void ClearDrawContent()                 | //rensar i consolfönstret Overload: area  |
| public static IList<string> CreateBoard()             | //skapar ett board med parametrar         |
| public static IList<string> RenderPieceBoard()        | //renderar spelpjäsbord med färger        |
| public static IList<string> RenderGameBoard()         | //renderar det gemensamma spelarbrädet    |
| public static IList<string> RenderGameBoardPieces()   | //rendarar spelpjäserna för spelarbrädet  |
| public static string UpdatePlayerPieceGFXByPosition() | //uppdaterar pjäser position grafiskt     |
| public static ConsoleColor BrushColor()               | //hjälpmetod för consolfärger             |

**Klassbeskrivning:** Hjälpklass som  hjälper till att positionera text och grafik på consolen samt skapar och renderar grafiska element som hör spelarpjäser och spelplaner till.