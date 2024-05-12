using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroClimate.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sensor_type",
                table: "station_readings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sensor_type_enum",
                table: "sensor_types",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sensor_type",
                table: "station_readings");

            migrationBuilder.DropColumn(
                name: "sensor_type_enum",
                table: "sensor_types");
        }
    }
}
