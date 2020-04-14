# Klassbeskrivning



## GameSession

| Variabler                                                  | Beskrivning |
| ---------------------------------------------------------- | ----------- |
| public int PlayerAmount                                    |             |
| public string PlayerName                                   |             |
| public string SessionName                                  |             |
| public IList<string> PlayerNames                           |             |
| public IList<string> Colors                                |             |
| public IList<Tuple<int, string, string>> SessionPlayerData |             |

| Metoder                                                   | Beskrivning |
| --------------------------------------------------------- | ----------- |
| public IGameSession InintializeSession()                  |             |
| public IGameSession SetSessionName()                      |             |
| public IGameSession SetPlayerAmount()                     |             |
| public IGameSession SetSessionData()                      |             |
| public IGameSession SaveState()                           |             |
| public  IGameSession StartGame()                          |             |
| public int GetPlayerAmount()                              |             |
| public string GetSessionName()                            |             |
| public IList<Tuple<int, string, string>> GetSessionData() |             |



Denna klassen implementerar interface IGameSession



## IGameSession

| Metoder                                            | Beskrivning |
| -------------------------------------------------- | ----------- |
| IGameSession InintializeSession()                  |             |
| IGameSession SetSessionName()                      |             |
| IGameSession SetPlayerAmount()                     |             |
| IGameSession SetSessionData()                      |             |
| IGameSession SaveState()                           |             |
| IGameSession StartGame()                           |             |
| string GetSessionName()                            |             |
| int GetPlayerAmount()                              |             |
| IList<Tuple<int, string, string>> GetSessionData() |             |

