using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_TblProfile_ProfileId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineItem_TblHamper_HamperId",
                table: "OrderLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineItem_Order_OrderId",
                table: "OrderLineItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLineItem",
                table: "OrderLineItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderLineItem");

            migrationBuilder.RenameTable(
                name: "OrderLineItem",
                newName: "TblOrderLineItem");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "TblOrder");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "TblAddress");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLineItem_HamperId",
                table: "TblOrderLineItem",
                newName: "IX_TblOrderLineItem_HamperId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ProfileId",
                table: "TblAddress",
                newName: "IX_TblAddress_ProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblOrderLineItem",
                table: "TblOrderLineItem",
                columns: new[] { "OrderId", "HamperId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblOrder",
                table: "TblOrder",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblAddress",
                table: "TblAddress",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblAddress_TblProfile_ProfileId",
                table: "TblAddress",
                column: "ProfileId",
                principalTable: "TblProfile",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderLineItem_TblHamper_HamperId",
                table: "TblOrderLineItem",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderLineItem_TblOrder_OrderId",
                table: "TblOrderLineItem",
                column: "OrderId",
                principalTable: "TblOrder",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblAddress_TblProfile_ProfileId",
                table: "TblAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderLineItem_TblHamper_HamperId",
                table: "TblOrderLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderLineItem_TblOrder_OrderId",
                table: "TblOrderLineItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblOrderLineItem",
                table: "TblOrderLineItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblOrder",
                table: "TblOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblAddress",
                table: "TblAddress");

            migrationBuilder.RenameTable(
                name: "TblOrderLineItem",
                newName: "OrderLineItem");

            migrationBuilder.RenameTable(
                name: "TblOrder",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "TblAddress",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_TblOrderLineItem_HamperId",
                table: "OrderLineItem",
                newName: "IX_OrderLineItem_HamperId");

            migrationBuilder.RenameIndex(
                name: "IX_TblAddress_ProfileId",
                table: "Address",
                newName: "IX_Address_ProfileId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderLineItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLineItem",
                table: "OrderLineItem",
                columns: new[] { "OrderId", "HamperId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_TblProfile_ProfileId",
                table: "Address",
                column: "ProfileId",
                principalTable: "TblProfile",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineItem_TblHamper_HamperId",
                table: "OrderLineItem",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineItem_Order_OrderId",
                table: "OrderLineItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
