using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movie_Api.Migrations
{
    /// <inheritdoc />
    public partial class addmovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenraId",
                table: "Genras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Storyline = table.Column<string>(type: "VARCHAR(2500)", maxLength: 2500, nullable: false),
                    Poster = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    GenraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genras_GenraId",
                        column: x => x.GenraId,
                        principalTable: "Genras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Genras",
                keyColumn: "Id",
                keyValue: 1,
                column: "GenraId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genras",
                keyColumn: "Id",
                keyValue: 2,
                column: "GenraId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genras",
                keyColumn: "Id",
                keyValue: 3,
                column: "GenraId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genras",
                keyColumn: "Id",
                keyValue: 4,
                column: "GenraId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genras",
                keyColumn: "Id",
                keyValue: 5,
                column: "GenraId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genras",
                keyColumn: "Id",
                keyValue: 6,
                column: "GenraId",
                value: null);

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenraId", "Poster", "Rate", "Storyline", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 5, null, 8.8000000000000007, "A skilled thief is given a chance at redemption if he can successfully perform an inception.", "Inception", 2010 },
                    { 2, 4, null, 7.5, "Paranormal investigators help a family terrorized by a dark presence.", "The Conjuring", 2013 },
                    { 3, 6, null, 7.7999999999999998, "A young couple falls in love on the ill-fated RMS Titanic.", "Titanic", 1997 },
                    { 4, 5, null, 8.6999999999999993, "A computer hacker learns about the true nature of his reality.", "The Matrix", 1999 },
                    { 5, 1, null, 9.0, "Batman faces the Joker, a criminal mastermind spreading chaos in Gotham.", "The Dark Knight", 2008 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genras_GenraId",
                table: "Genras",
                column: "GenraId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenraId",
                table: "Movies",
                column: "GenraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genras_Genras_GenraId",
                table: "Genras",
                column: "GenraId",
                principalTable: "Genras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genras_Genras_GenraId",
                table: "Genras");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Genras_GenraId",
                table: "Genras");

            migrationBuilder.DropColumn(
                name: "GenraId",
                table: "Genras");
        }
    }
}
