using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameEngine.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piece",
                columns: table => new
                {
                    PieceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piece", x => x.PieceID);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionName = table.Column<string>(nullable: true),
                    GameFinished = table.Column<bool>(nullable: false),
                    Winner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPiece",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    PieceId = table.Column<int>(nullable: false),
                    PieceFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPiece", x => new { x.PlayerId, x.PieceId });
                    table.ForeignKey(
                        name: "FK_PlayerPiece_Piece_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Piece",
                        principalColumn: "PieceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPiece_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSession",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    Color = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSession", x => new { x.SessionId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayerSession_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSession_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPiece_PieceId",
                table: "PlayerPiece",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSession_PlayerId",
                table: "PlayerSession",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPiece");

            migrationBuilder.DropTable(
                name: "PlayerSession");

            migrationBuilder.DropTable(
                name: "Piece");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Session");
        }
    }
}
