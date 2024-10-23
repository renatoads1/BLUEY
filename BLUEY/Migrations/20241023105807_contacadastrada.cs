using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLUEY.Migrations
{
    /// <inheritdoc />
    public partial class contacadastrada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CONTACADASTRADA",
                table: "lCTOFISConsServs",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TABELACTBFISLCTOCTB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CODIGOEMPRESA = table.Column<int>(type: "int", nullable: false),
                    CONTACTB = table.Column<int>(type: "int", nullable: false),
                    CODIGOHISTCTB = table.Column<int>(type: "int", nullable: false),
                    COMPLHISTCTB = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LCTOFISConsServId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TABELACTBFISLCTOCTB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TABELACTBFISLCTOCTB_lCTOFISConsServs_LCTOFISConsServId",
                        column: x => x.LCTOFISConsServId,
                        principalTable: "lCTOFISConsServs",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TABELACTBFISLCTOCTB_LCTOFISConsServId",
                table: "TABELACTBFISLCTOCTB",
                column: "LCTOFISConsServId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TABELACTBFISLCTOCTB");

            migrationBuilder.DropColumn(
                name: "CONTACADASTRADA",
                table: "lCTOFISConsServs");
        }
    }
}
