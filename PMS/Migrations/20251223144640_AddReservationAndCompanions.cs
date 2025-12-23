using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationAndCompanions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheckedOut",
                table: "Reservations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingSource",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DepositAmount",
                table: "Reservations",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfGuests",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Reservations",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequests",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companion_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companion_ReservationId",
                table: "Companion",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companion");

            migrationBuilder.DropColumn(
                name: "BookingSource",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "NumberOfGuests",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "SpecialRequests",
                table: "Reservations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutDate",
                table: "Reservations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckedOut",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
