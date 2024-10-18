using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLUEY.Migrations
{
    /// <inheritdoc />
    public partial class ContaContabil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lCTOFISConsServs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EMPRESA_ = table.Column<int>(type: "int", nullable: false),
                    CHAVE = table.Column<int>(type: "int", nullable: false),
                    FILIAL = table.Column<int>(type: "int", nullable: false),
                    COD_PESSOA = table.Column<int>(type: "int", nullable: false),
                    INSCR_FEDERAL = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NOME = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VALOR = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CFOP = table.Column<int>(type: "int", nullable: false),
                    TABELA = table.Column<int>(type: "int", nullable: false),
                    NUMERONF = table.Column<int>(type: "int", nullable: false),
                    MOVIMENTO = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CONTACONTABIL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lCTOFISConsServs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lCTOFISConsServs");
        }
    }
}
