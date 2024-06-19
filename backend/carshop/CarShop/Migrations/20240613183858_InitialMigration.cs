using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarShop.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    image_url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    doc_type = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    doc_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    phonenumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "financial_transactions",
                columns: table => new
                {
                    financial_transation_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    type_transation = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    categoria_id = table.Column<int>(type: "integer", nullable: false),
                    CustomerDBId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_transactions", x => x.financial_transation_id);
                    table.ForeignKey(
                        name: "FK_financial_transactions_customers_CustomerDBId",
                        column: x => x.CustomerDBId,
                        principalTable: "customers",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_financial_transactions_CustomerDBId",
                table: "financial_transactions",
                column: "CustomerDBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financial_transactions");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
