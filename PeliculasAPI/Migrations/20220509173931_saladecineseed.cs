using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace PeliculasAPI.Migrations
{
    public partial class saladecineseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SalaDeCine",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 10, "Sambil", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-17.6966808 -63.1678671)") });

            migrationBuilder.InsertData(
                table: "SalaDeCine",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 11, "Megacentro", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-17.6966708 -63.167971)") });

            migrationBuilder.InsertData(
                table: "SalaDeCine",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 12, "Village East Cinema", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-17.6966608 -63.1678071)") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SalaDeCine",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SalaDeCine",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SalaDeCine",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
