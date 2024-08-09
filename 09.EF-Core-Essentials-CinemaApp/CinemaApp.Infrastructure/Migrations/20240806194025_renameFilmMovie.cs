using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Migrations
{
    public partial class renameFilmMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Films_FilmId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_Films_FilmId",
                table: "Tariffs");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.RenameColumn(
                name: "FilmId",
                table: "Tariffs",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Tariffs_FilmId",
                table: "Tariffs",
                newName: "IX_Tariffs_MovieId");

            migrationBuilder.RenameColumn(
                name: "FilmId",
                table: "Schedules",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_FilmId",
                table: "Schedules",
                newName: "IX_Schedules_MovieId");

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Movies_MovieId",
                table: "Schedules",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_Movies_MovieId",
                table: "Tariffs",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Movies_MovieId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_Movies_MovieId",
                table: "Tariffs");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Tariffs",
                newName: "FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_Tariffs_MovieId",
                table: "Tariffs",
                newName: "IX_Tariffs_FilmId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Schedules",
                newName: "FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_MovieId",
                table: "Schedules",
                newName: "IX_Schedules_FilmId");

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Films_FilmId",
                table: "Schedules",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_Films_FilmId",
                table: "Tariffs",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
