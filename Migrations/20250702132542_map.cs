using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Api.Migrations
{
    /// <inheritdoc />
    public partial class map : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genras_Genras_GenraId",
                table: "Genras");

            migrationBuilder.DropIndex(
                name: "IX_Genras_GenraId",
                table: "Genras");

            migrationBuilder.DropColumn(
                name: "GenraId",
                table: "Genras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenraId",
                table: "Genras",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Genras_GenraId",
                table: "Genras",
                column: "GenraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genras_Genras_GenraId",
                table: "Genras",
                column: "GenraId",
                principalTable: "Genras",
                principalColumn: "Id");
        }
    }
}
