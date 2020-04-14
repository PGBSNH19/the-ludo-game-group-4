

| IGameSession Interface till GameSession             |
| --------------------------------------------------- |
| IGameSession InintializeSession();                  |
| IGameSession SetSessionName();                      |
| IGameSession SetPlayerAmount();                     |
| IGameSession SetSessionData();                      |
| IGameSession SaveState();                           |
| IGameSession StartGame();                           |
| string GetSessionName();                            |
| int GetPlayerAmount();                              |
| IList<Tuple<int, string, string>> GetSessionData(); |
| **Beskrivning:**  Fluent API till GameSession       |





| GameSession                                                |
| ---------------------------------------------------------- |
| InintializeSession();                                      |
| SetSessionName();                                          |
| SetPlayerAmount();                                         |
| SetSessionData();                                          |
| SaveState();                                               |
| StartGame();                                               |
| string GetSessionName();                                   |
| int GetPlayerAmount();                                     |
| IList<Tuple<int, string, string>> GetSessionData();        |
| **Beskrivning:**  Ställer in all data innan spelet börjar. |

