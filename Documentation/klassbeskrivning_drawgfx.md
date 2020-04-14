# Klassbeskrivning



## DrawGFX

<<<<<<< HEAD
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
=======
| Enum                     | Beskrivning |
| ------------------------ | ----------- |
| public enum BoardGFXItem |             |
| public enum PieceGFXItem |             |
| public enum GameColors   |             |

| Variabler                                | Beskrivning |
| ---------------------------------------- | ----------- |
| public static string GameBoardItem       |             |
| public static string PieceBoardItem      |             |
| public static string GameBoardPiecesItem |             |
| public static string PieceOnBoard        |             |
| public static string PieceInNest         |             |
>>>>>>> 95b94db7d511c8b22ff4b514a9ba46aa3e2f5bad

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