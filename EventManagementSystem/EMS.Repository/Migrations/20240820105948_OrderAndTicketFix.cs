using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class OrderAndTicketFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Orders_OrderId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_OrderId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Tickets",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Tickets",
                newName: "price");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OrderId",
                table: "Tickets",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Orders_OrderId",
                table: "Tickets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
