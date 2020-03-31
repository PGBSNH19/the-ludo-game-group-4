Player

| Responsibility | Collaborators |
| -------------- | ------------- |
| Name           | GameBoard     |
| Color          | GamePiece     |
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

Dice

| Responsibility | Collaborators |
| -------------- | ------------- |
| Roll           | Player        |
|                | GamePiece     |
|                |               |

