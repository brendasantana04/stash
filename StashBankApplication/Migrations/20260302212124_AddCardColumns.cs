using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StashBankApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddCardColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_user_user_id",
                table: "account");

            migrationBuilder.DropIndex(
                name: "IX_account_user_id",
                table: "account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_account_user_id",
                table: "account",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_account_user_user_id",
                table: "account",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
