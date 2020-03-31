Player

| Responsibility | Collaborators |
| -------------- | ------------- |
| Name           | GameBoard     |
| Color          | GamePiece     |
| Dice           |               |
| HighScore      |               |

GamePiece

| Responsibility  | Collaborators |
| --------------- | ------------- |
| PieceMovement   | Player        |
| PieceKnockedOut | GameBoard     |
| PieceFinished   |               |

GameBoard

| Responsibility | Collaborators |
| -------------- | ------------- |
| PiecePosition  | Player        |
| PlayerTurn     | GamePiece     |
| Winner         |               |
