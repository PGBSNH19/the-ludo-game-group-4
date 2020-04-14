# Klassbeskrivning



## GameSession

| Variabler                                                  | Beskrivning                                                  |
| ---------------------------------------------------------- | ------------------------------------------------------------ |
| public int PlayerAmount                                    | //antal spelare                                              |
| public string PlayerName                                   | //spelarnamn                                                 |
| public string SessionName                                  | //namn på sessionen                                          |
| public IList<string> PlayerNames                           | //lista där spelarnamn lagras                                |
| public IList<string> Colors                                | //lista med färger                                           |
| public IList<Tuple<int, string, string>> SessionPlayerData | //lagring av sessionsdata för överföring till databas samt GameBoard |

| Metoder                                                   | Beskrivning                                                  |
| --------------------------------------------------------- | ------------------------------------------------------------ |
| public IGameSession InintializeSession()                  | //skapar en gamesession                                      |
| public IGameSession SetSessionName()                      | //tilldela sessionsnamn                                      |
| public IGameSession SetPlayerAmount()                     | //välj antal spelare                                         |
| public IGameSession SetSessionData()                      | //lägger in tidigare inskriven data i IList<Tuple<int, string, string>> |
| public IGameSession SaveState()                           | //sparar datan till databasen                                |
| public  IGameSession StartGame()                          | //returnerar det slutgiltiga objektet                        |
| public int GetPlayerAmount()                              | //metod för att komma åt spelarantal från från andra klasser, tex GameBoard |
| public string GetSessionName()                            | //metod för att komma åt sessionsnamn från andra klasser, tex GameBoard |
| public IList<Tuple<int, string, string>> GetSessionData() | //metod för att komma åt sessionsdata (id, spelarnamn, vald färg) från andra klasser, tex GameBoard |

**Klassbeskrivning:** Implementerar interfacet IGameSession. När new game väljs i startmenyn, skapas en ny spelsession som läggs in som parameter i konstruktorn i en  ny GameBoard. Sparar även den nya sessionen till databasen.





## IGameSession

| Metoder                                            | Beskrivning      |
| -------------------------------------------------- | ---------------- |
| IGameSession InintializeSession()                  | //se GameSession |
| IGameSession SetSessionName()                      | //se GameSession |
| IGameSession SetPlayerAmount()                     | //se GameSession |
| IGameSession SetSessionData()                      | //se GameSession |
| IGameSession SaveState()                           | //se GameSession |
| IGameSession StartGame()                           | //se GameSession |
| string GetSessionName()                            | //se GameSession |
| int GetPlayerAmount()                              | //se GameSession |
| IList<Tuple<int, string, string>> GetSessionData() | //se GameSession |

**Klassbeskrivning:** Fluent API till GameSession