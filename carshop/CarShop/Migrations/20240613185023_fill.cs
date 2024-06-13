using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.Migrations
{
    public partial class fill : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO clients(name, image_url, doc_type, doc_number, phonenumber)" +
            "VALUES('João Silva', '/images/joao_silva.jpg', 'CPF', '123.456.789-00', '5511900001111')," +
            "('Maria Oliveira', '/maria_oliveira.jpg', 'CPF', '987.654.321-00', '5511988882222')," +
            "('Carlos Pereira', '/images/carlos_pereira.jpg', 'CNPJ', '12.345.678/0001-99', '5511977773333')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("TRUNCATE clients");
        }
    }
}
