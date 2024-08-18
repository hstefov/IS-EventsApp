using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AttendeeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "AttendeeId",
                table: "Orders",
                newName: "UserAttendeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AttendeeId",
                table: "Orders",
                newName: "IX_Orders_UserAttendeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserAttendeeId",
                table: "Orders",
                column: "UserAttendeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserAttendeeId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserAttendeeId",
                table: "Orders",
                newName: "AttendeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserAttendeeId",
                table: "Orders",
                newName: "IX_Orders_AttendeeId");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AttendeeId",
                table: "Orders",
                column: "AttendeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
