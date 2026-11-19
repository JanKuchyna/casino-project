using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Casino.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnumLookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BetTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Draw" },
                    { 1, "Home" },
                    { 2, "Away" }
                });

            migrationBuilder.InsertData(
                table: "EventResults",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "HomeWin" },
                    { 1, "Draw" },
                    { 2, "AwayWin" }
                });

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Pending" },
                    { 1, "Won" },
                    { 2, "Lost" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Deposit" },
                    { 1, "Withdrawal" },
                    { 2, "BetStake" },
                    { 3, "BetWin" },
                    { 4, "CasinoStake" },
                    { 5, "CasinoWin" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Customer" },
                    { 1, "Bookmaker" },
                    { 2, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_TransactionType",
                table: "WalletTransactions",
                column: "TransactionType");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Status",
                table: "Tickets",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SportEvents_Result",
                table: "SportEvents",
                column: "Result");

            migrationBuilder.CreateIndex(
                name: "IX_Odds_BetType",
                table: "Odds",
                column: "BetType");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Role",
                table: "AspNetUsers",
                column: "Role");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserRoles_Role",
                table: "AspNetUsers",
                column: "Role",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Odds_BetTypes_BetType",
                table: "Odds",
                column: "BetType",
                principalTable: "BetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportEvents_EventResults_Result",
                table: "SportEvents",
                column: "Result",
                principalTable: "EventResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketStatuses_Status",
                table: "Tickets",
                column: "Status",
                principalTable: "TicketStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_TransactionTypes_TransactionType",
                table: "WalletTransactions",
                column: "TransactionType",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserRoles_Role",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Odds_BetTypes_BetType",
                table: "Odds");

            migrationBuilder.DropForeignKey(
                name: "FK_SportEvents_EventResults_Result",
                table: "SportEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketStatuses_Status",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_TransactionTypes_TransactionType",
                table: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "BetTypes");

            migrationBuilder.DropTable(
                name: "EventResults");

            migrationBuilder.DropTable(
                name: "TicketStatuses");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_WalletTransactions_TransactionType",
                table: "WalletTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_Status",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_SportEvents_Result",
                table: "SportEvents");

            migrationBuilder.DropIndex(
                name: "IX_Odds_BetType",
                table: "Odds");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Role",
                table: "AspNetUsers");
        }
    }
}
