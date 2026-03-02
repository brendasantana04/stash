using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StashBankApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixCardRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_card_account_AccountId",
                table: "card");

            migrationBuilder.RenameColumn(
                name: "limit",
                table: "card",
                newName: "credit_limit");

            migrationBuilder.RenameColumn(
                name: "issued_at",
                table: "card",
                newName: "last_upgrade_at");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "card",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_card_AccountId",
                table: "card",
                newName: "IX_card_account_id");

            migrationBuilder.AlterColumn<int>(
                name: "tier",
                table: "card",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "card",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bool");

            migrationBuilder.AlterColumn<decimal>(
                name: "available_credit",
                table: "card",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)");

            migrationBuilder.AlterColumn<decimal>(
                name: "credit_limit",
                table: "card",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_upgrade_at",
                table: "card",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_card_account_account_id",
                table: "card",
                column: "account_id",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_card_account_account_id",
                table: "card");

            migrationBuilder.RenameColumn(
                name: "last_upgrade_at",
                table: "card",
                newName: "issued_at");

            migrationBuilder.RenameColumn(
                name: "credit_limit",
                table: "card",
                newName: "limit");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "card",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_card_account_id",
                table: "card",
                newName: "IX_card_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "tier",
                table: "card",
                type: "varchar(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "card",
                type: "bool",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "available_credit",
                table: "card",
                type: "varchar(16)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "issued_at",
                table: "card",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "limit",
                table: "card",
                type: "varchar(16)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_card_account_AccountId",
                table: "card",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
