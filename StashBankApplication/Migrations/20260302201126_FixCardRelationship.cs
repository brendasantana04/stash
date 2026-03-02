using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StashBankApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixCardRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transfer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_account_to = table.Column<long>(type: "bigint", nullable: false),
                    id_account_from = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_on = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    created_on = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(40)", nullable: false),
                    funds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    active = table.Column<bool>(type: "bool", nullable: false),
                    created_on = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    tier = table.Column<string>(type: "varchar(16)", nullable: false),
                    limit = table.Column<string>(type: "varchar(16)", nullable: false),
                    available_credit = table.Column<string>(type: "varchar(16)", nullable: false),
                    issued_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bool", nullable: false),
                    created_on = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.id);
                    table.ForeignKey(
                        name: "FK_card_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountid = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_on = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_transaction_account_accountid",
                        column: x => x.accountid,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_user_id",
                table: "account",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_card_AccountId",
                table: "card",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_accountid",
                table: "transaction",
                column: "accountid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "transfer");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
