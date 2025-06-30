using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolXMLIntegracao.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contadores",
                columns: table => new
                {
                    cpf_cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    senha = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contadores", x => x.cpf_cnpj);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    razao_social = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    fantasia = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.cnpj);
                });

            migrationBuilder.CreateTable(
                name: "documentos",
                columns: table => new
                {
                    chave_acesso = table.Column<string>(type: "character varying(44)", maxLength: 44, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    tipo = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    conteudo = table.Column<string>(type: "text", nullable: true),
                    data_autorizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentos", x => x.chave_acesso);
                    table.ForeignKey(
                        name: "FK_documentos_empresas_cnpj",
                        column: x => x.cnpj,
                        principalTable: "empresas",
                        principalColumn: "cnpj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empresa_contador",
                columns: table => new
                {
                    cnpj_empresa = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    cnpj_contador = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa_contador", x => new { x.cnpj_empresa, x.cnpj_contador });
                    table.ForeignKey(
                        name: "FK_empresa_contador_contadores_cnpj_contador",
                        column: x => x.cnpj_contador,
                        principalTable: "contadores",
                        principalColumn: "cpf_cnpj",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_empresa_contador_empresas_cnpj_empresa",
                        column: x => x.cnpj_empresa,
                        principalTable: "empresas",
                        principalColumn: "cnpj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contadores_cpf_cnpj",
                table: "contadores",
                column: "cpf_cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_documentos_chave_acesso",
                table: "documentos",
                column: "chave_acesso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_documentos_cnpj",
                table: "documentos",
                column: "cnpj");

            migrationBuilder.CreateIndex(
                name: "IX_empresa_contador_cnpj_contador",
                table: "empresa_contador",
                column: "cnpj_contador");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_cnpj",
                table: "empresas",
                column: "cnpj",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documentos");

            migrationBuilder.DropTable(
                name: "empresa_contador");

            migrationBuilder.DropTable(
                name: "contadores");

            migrationBuilder.DropTable(
                name: "empresas");
        }
    }
}
