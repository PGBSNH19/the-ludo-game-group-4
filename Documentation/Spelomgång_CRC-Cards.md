Player

| Responsibility | Collaborators |
| -------------- | ------------- |
| Name           | GameBoard     |
| Color          | GamePiece     |
| Dice           |               |

GamePiece

| Responsibility | Collaborators |
| -------------- | ------------- |
| PieceMovement  | Player        |
|                | GameBoard     |
|                |               |

GameBoard

| Responsibility | Collaborators |
| -------------- | ------------- |
| PiecePosition  | Player        |
| PlayerTurn     | GamePiece     |
|                |               |

