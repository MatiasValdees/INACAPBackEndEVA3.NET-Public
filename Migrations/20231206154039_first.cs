using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVA3.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblMusic",
                columns: table => new
                {
                    IdCancion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Album = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMusic", x => x.IdCancion);
                });

            migrationBuilder.CreateTable(
                name: "TblPlayList",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPlayList", x => x.PlaylistId);
                });

            migrationBuilder.CreateTable(
                name: "TblRelPlayListMusic",
                columns: table => new
                {
                    PlayListId = table.Column<int>(type: "int", nullable: false),
                    MusicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRelPlayListMusic", x => new { x.PlayListId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_TblRelPlayListMusic_TblMusic_MusicId",
                        column: x => x.MusicId,
                        principalTable: "TblMusic",
                        principalColumn: "IdCancion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblRelPlayListMusic_TblPlayList_PlayListId",
                        column: x => x.PlayListId,
                        principalTable: "TblPlayList",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblRelPlayListMusic_MusicId",
                table: "TblRelPlayListMusic",
                column: "MusicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblRelPlayListMusic");

            migrationBuilder.DropTable(
                name: "TblMusic");

            migrationBuilder.DropTable(
                name: "TblPlayList");
        }
    }
}
