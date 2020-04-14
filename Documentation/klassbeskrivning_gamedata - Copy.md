# Class and description

## GameData

variables and description
DataContext Context  = an instans of DataContext class which inheretfrom DbContext class. this is used to save data to database.           
public int SessionId = it holds sessionId for foreign key in join tables.
public int PlayerId = it holds playerId for forein key in join table.         
public int CheckSessionCreated = it will check if new session creats or not. 

methods and description
public void InsertSessionData()= saving session name to session table    

public void InsertEachPlayerData()= giving each player 4 pieces and getting user input and send it to other metod to save them in DB.  
public void InsertPlayerData()= saving player information to player table

public void InsertPieceData()= saving piece data to piece table    
public void ShowHighScore()= shows players with high scores 
public void UpdatePiecePosition()= update piece position 
public void UpdateWinner() = update the winner row in session table with winner name of the session.
public List<string> ShowAllSession()= shows all session that are available for playing.
public List<MyGameData> LoadGame()= read all data from database to give user ability to continue play a session.

# sub Klass och egenskaper 

##MyGameData

public int playerID 
public string SessionName
public string PlayerName
public string Color
public int PieceID
public int Position

description= this sub class is used to create a list of object and return the game data for user to continnue play. 