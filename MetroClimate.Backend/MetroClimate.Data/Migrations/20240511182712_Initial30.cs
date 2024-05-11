using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroClimate.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_station_readings_sensor_id",
                table: "station_readings",
                column: "sensor_id");

            migrationBuilder.AddForeignKey(
                name: "fk_station_readings_sensors_sensor_id",
                table: "station_readings",
                column: "sensor_id",
                principalTable: "sensors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_station_readings_sensors_sensor_id",
                table: "station_readings");

            migrationBuilder.DropIndex(
                name: "ix_station_readings_sensor_id",
                table: "station_readings");
        }
    }
}
