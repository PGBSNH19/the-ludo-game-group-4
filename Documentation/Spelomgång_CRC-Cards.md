Player

| Responsibility        | Collaborators |
| --------------------- | ------------- |
| string Name           | GameBoard     |
| int/enum/string Color | GamePiece     |
| bool WinGame          | Database      |

PlayerProfile

| Responsibility | Collaborators |
| -------------- | ------------- |
| string Name    | Player        |
| int Wins       | Database      |
|                |               |

GamePiece

| Responsibility     | Collaborators |
| ------------------ | ------------- |
| int LocalPosition  | Player        |
| bool PieceFinished | GameBoard     |
|                    | Database      |

GameBoard

| Responsibility              | Collaborators |
| --------------------------- | ------------- |
| PiecePosition               | Player        |
| PlayerTurn                  | GamePiece     |
| bool PositionOccupied       | Database      |
| AmountPiece in center       |               |
| Winner                      |               |
| PieceKnockedOut             |               |
| int CoordinateOuterPosition |               |
| int CoordinateInnerPosition |               |
| FirstTurnInGame             |               |
| RollSix                     |               |
| MovePiece                   |               |

Dice

| Responsibility | Collaborators |
| -------------- | ------------- |
| int DiceNumber | Player        |
| Roll()         | GamePiece     |
|                |               |

