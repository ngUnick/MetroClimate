using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroClimate.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sensor_type",
                table: "station_readings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sensor_type",
                table: "station_readings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
