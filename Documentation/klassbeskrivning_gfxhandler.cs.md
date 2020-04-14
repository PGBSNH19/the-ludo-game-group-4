# Klassbeskrivning

## BoardGFXItem

| Enum                    | Beskrivning                                                  |
| ----------------------- | ------------------------------------------------------------ |
| enum BoardGFXItem       | //enkapsulerar typ av grafik för gemensamt spelbräde som parameter när man anropar CreateBoard() |
| enum PieceGFXItem       | //enkapsulerar typ av grafik för spelpjäsbräde som parameter när man anropar CreateBoard() |
| enum GameBoardPiecesGFX | //enkapsulerar typ av grafik för gemensamt spelbräde för alla spelares spelpjäser som parameter när man anropar CreateBoard() |

**Klassbeskrivning:** Används för enkapsulering av parameter i CreateBoard()



## PieceGFXItem

| Enum               | Beskrivning                                                  |
| ------------------ | ------------------------------------------------------------ |
| enum PieceOnBBoard | //enkapsulerar typ av grafik för spelpjäs mitt på något spelbräde |
| enum PieceInNest   | //enkapsulerar typ av grafik för spelpjäs i sitt bo          |

**Klassbeskrivning:** Används för grafik i UpdatePlayerPieceGFXByPosition()



## GameColors

| Enum        | Beskrivning                    |
| ----------- | ------------------------------ |
| enum Red    | //enkapsulerar spelfärgen röd  |
| enum Blue   | //enkapsulerar spelfärgen blå  |
| enum Green  | //enkapsulerar spelfärgen grön |
| enum Yellow | //enkapsulerar spelfärgen gul  |

**Klassbeskrivning:** Används  i olika delar av programmet



## DrawGFX

| Variabler                                | Beskrivning                                             |
| ---------------------------------------- | ------------------------------------------------------- |
| public static string GameBoardItem       | //grafik för GameBoard                                  |
| public static string PieceBoardItem      | //grafik för PieceBoard                                 |
| public static string GameBoardPiecesItem | //grafik för spelarpjäser                               |
| public static string PieceOnBoard        | //grafik för spelarpjäser på GameBoard                  |
| public static string PieceInNest         | //grafik för spelarpjäsen då den befinner sig i sitt bo |

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