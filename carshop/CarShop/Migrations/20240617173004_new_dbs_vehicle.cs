using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarShop.Migrations
{
    public partial class new_dbs_vehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transactions_clients_ClientDBId",
                table: "financial_transactions");

            migrationBuilder.DropIndex(
                name: "IX_financial_transactions_ClientDBId",
                table: "financial_transactions");

            migrationBuilder.DropColumn(
                name: "ClientDBId",
                table: "financial_transactions");

            migrationBuilder.AlterColumn<string>(
                name: "type_transation",
                table: "financial_transactions",
                type: "character varying(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    vehicle_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    renavan = table.Column<string>(type: "varchar(12)", nullable: false),
                    license_plate = table.Column<string>(type: "varchar(8)", nullable: false),
                    brand = table.Column<string>(type: "varchar(40)", nullable: false),
                    model = table.Column<string>(type: "varchar(40)", nullable: false),
                    model_year = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    vehicle_type = table.Column<string>(type: "text", nullable: false),
                    year_manufacture = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    registration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    change_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    situation = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.vehicle_id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_image",
                columns: table => new
                {
                    vehicle_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "varchar(300)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_image", x => x.vehicle_id);
                    table.ForeignKey(
                        name: "FK_vehicle_image_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_financial_transactions_categoria_id",
                table: "financial_transactions",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_image_vehicle_id",
                table: "vehicle_image",
                column: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transactions_clients_categoria_id",
                table: "financial_transactions",
                column: "categoria_id",
                principalTable: "clients",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transactions_clients_categoria_id",
                table: "financial_transactions");

            migrationBuilder.DropTable(
                name: "vehicle_image");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropIndex(
                name: "IX_financial_transactions_categoria_id",
                table: "financial_transactions");

            migrationBuilder.AlterColumn<string>(
                name: "type_transation",
                table: "financial_transactions",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<int>(
                name: "ClientDBId",
                table: "financial_transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_financial_transactions_ClientDBId",
                table: "financial_transactions",
                column: "ClientDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transactions_clients_ClientDBId",
                table: "financial_transactions",
                column: "ClientDBId",
                principalTable: "clients",
                principalColumn: "client_id");
        }
    }
}
