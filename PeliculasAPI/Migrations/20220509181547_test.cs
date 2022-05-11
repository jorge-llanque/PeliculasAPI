using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasAPI.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCines_SalaDeCine_SalaDeCineId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaDeCine",
                table: "SalaDeCine");

            migrationBuilder.RenameTable(
                name: "SalaDeCine",
                newName: "SalaDeCines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaDeCines",
                table: "SalaDeCines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCines_SalaDeCines_SalaDeCineId",
                table: "PeliculasSalasDeCines",
                column: "SalaDeCineId",
                principalTable: "SalaDeCines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCines_SalaDeCines_SalaDeCineId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaDeCines",
                table: "SalaDeCines");

            migrationBuilder.RenameTable(
                name: "SalaDeCines",
                newName: "SalaDeCine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaDeCine",
                table: "SalaDeCine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCines_SalaDeCine_SalaDeCineId",
                table: "PeliculasSalasDeCines",
                column: "SalaDeCineId",
                principalTable: "SalaDeCine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
