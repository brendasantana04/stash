using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StashBankApplication.Migrations
{
    /// <inheritdoc />
    public partial class CreateSavingsBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_card_account_account_id",
                table: "card");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "card",
                newName: "accountId");

            migrationBuilder.RenameIndex(
                name: "IX_card_account_id",
                table: "card",
                newName: "IX_card_accountId");

            migrationBuilder.CreateTable(
                name: "SavingsBox",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_on = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsBox", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_card_account_accountId",
                table: "card",
                column: "accountId",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_card_account_accountId",
                table: "card");

            migrationBuilder.DropTable(
                name: "SavingsBox");

            migrationBuilder.RenameColumn(
                name: "accountId",
                table: "card",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_card_accountId",
                table: "card",
                newName: "IX_card_account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_card_account_account_id",
                table: "card",
                column: "account_id",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
