using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Banking.Infrastructure.Migrations
{
    public partial class AddTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statement_Account_AccountId",
                table: "Statement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statement",
                table: "Statement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Statement",
                newName: "statement");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "account");

            migrationBuilder.RenameIndex(
                name: "IX_Statement_AccountId",
                table: "statement",
                newName: "IX_statement_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_statement",
                table: "statement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_account",
                table: "account",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderStatementId = table.Column<int>(nullable: false),
                    PayeeStatementId = table.Column<int>(nullable: false),
                    TransferAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_statement_PayeeStatementId",
                        column: x => x.PayeeStatementId,
                        principalTable: "statement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_transaction_statement_SenderStatementId",
                        column: x => x.SenderStatementId,
                        principalTable: "statement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_PayeeStatementId",
                table: "transaction",
                column: "PayeeStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_SenderStatementId",
                table: "transaction",
                column: "SenderStatementId");

            migrationBuilder.AddForeignKey(
                name: "FK_statement_account_AccountId",
                table: "statement",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_statement_account_AccountId",
                table: "statement");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_statement",
                table: "statement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_account",
                table: "account");

            migrationBuilder.RenameTable(
                name: "statement",
                newName: "Statement");

            migrationBuilder.RenameTable(
                name: "account",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_statement_AccountId",
                table: "Statement",
                newName: "IX_Statement_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statement",
                table: "Statement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statement_Account_AccountId",
                table: "Statement",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
