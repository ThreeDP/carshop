using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.Migrations
{
    public partial class new_dbs_vehicle_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transactions_clients_categoria_id",
                table: "financial_transactions");

            migrationBuilder.RenameColumn(
                name: "categoria_id",
                table: "financial_transactions",
                newName: "client_id");

            migrationBuilder.RenameIndex(
                name: "IX_financial_transactions_categoria_id",
                table: "financial_transactions",
                newName: "IX_financial_transactions_client_id");

            migrationBuilder.AddColumn<int>(
                name: "vehicle_id",
                table: "financial_transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_financial_transactions_vehicle_id",
                table: "financial_transactions",
                column: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transactions_clients_client_id",
                table: "financial_transactions",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transactions_vehicles_vehicle_id",
                table: "financial_transactions",
                column: "vehicle_id",
                principalTable: "vehicles",
                principalColumn: "vehicle_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transactions_clients_client_id",
                table: "financial_transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_financial_transactions_vehicles_vehicle_id",
                table: "financial_transactions");

            migrationBuilder.DropIndex(
                name: "IX_financial_transactions_vehicle_id",
                table: "financial_transactions");

            migrationBuilder.DropColumn(
                name: "vehicle_id",
                table: "financial_transactions");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "financial_transactions",
                newName: "categoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_financial_transactions_client_id",
                table: "financial_transactions",
                newName: "IX_financial_transactions_categoria_id");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transactions_clients_categoria_id",
                table: "financial_transactions",
                column: "categoria_id",
                principalTable: "clients",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
