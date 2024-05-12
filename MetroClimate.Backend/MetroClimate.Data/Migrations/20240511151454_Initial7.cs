using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroClimate.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sensors_sensor_types_type_id",
                table: "sensors");

            migrationBuilder.DropIndex(
                name: "ix_sensors_type_id",
                table: "sensors");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "sensors");

            migrationBuilder.AddColumn<int>(
                name: "sensor_type_id",
                table: "sensors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_sensors_sensor_type_id",
                table: "sensors",
                column: "sensor_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sensors_sensor_types_sensor_type_id",
                table: "sensors",
                column: "sensor_type_id",
                principalTable: "sensor_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sensors_sensor_types_sensor_type_id",
                table: "sensors");

            migrationBuilder.DropIndex(
                name: "ix_sensors_sensor_type_id",
                table: "sensors");

            migrationBuilder.DropColumn(
                name: "sensor_type_id",
                table: "sensors");

            migrationBuilder.AddColumn<int>(
                name: "type_id",
                table: "sensors",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_sensors_type_id",
                table: "sensors",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sensors_sensor_types_type_id",
                table: "sensors",
                column: "type_id",
                principalTable: "sensor_types",
                principalColumn: "id");
        }
    }
}
