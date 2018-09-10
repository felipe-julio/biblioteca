using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Migrations
{
    public partial class biblioteca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "cliente",
                schema: "dbo",
                columns: table => new
                {
                    cd_cliente = table.Column<Guid>(nullable: false),
                    nm_cliente = table.Column<string>(nullable: false),
                    cpf_cliente = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.cd_cliente);
                });

            migrationBuilder.CreateTable(
                name: "livro",
                schema: "dbo",
                columns: table => new
                {
                    cd_livro = table.Column<Guid>(nullable: false),
                    ti_livro = table.Column<string>(nullable: false),
                    at_livro = table.Column<string>(nullable: false),
                    st_livro = table.Column<char>(nullable: false),
                    ed_livro = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livro", x => x.cd_livro);
                });

            migrationBuilder.CreateTable(
                name: "emprestimo",
                schema: "dbo",
                columns: table => new
                {
                    cd_emprestimo = table.Column<Guid>(nullable: false),
                    cd_cliente = table.Column<Guid>(nullable: false),
                    cd_livro = table.Column<Guid>(nullable: false),
                    data_in_emprestimo = table.Column<DateTime>(nullable: false),
                    data_dev_emprestimo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emprestimo", x => x.cd_emprestimo);
                    table.ForeignKey(
                        name: "FK_emprestimo_cliente_cd_cliente",
                        column: x => x.cd_cliente,
                        principalSchema: "dbo",
                        principalTable: "cliente",
                        principalColumn: "cd_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_emprestimo_livro_cd_livro",
                        column: x => x.cd_livro,
                        principalSchema: "dbo",
                        principalTable: "livro",
                        principalColumn: "cd_livro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emprestimo_cd_cliente",
                schema: "dbo",
                table: "emprestimo",
                column: "cd_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_emprestimo_cd_livro",
                schema: "dbo",
                table: "emprestimo",
                column: "cd_livro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emprestimo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "cliente",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "livro",
                schema: "dbo");
        }
    }
}
