using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorUsuario.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandochaveestrangeiranacolunapapelUsuarioeStatusUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_PapelUsuario_PapelUsuarioId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_StatusUsuario_StatusUsuarioId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_PapelUsuarioId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_StatusUsuarioId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "PapelUsuarioId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "StatusUsuarioId",
                table: "Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdPapelUsuario",
                table: "Usuario",
                column: "IdPapelUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdStatusUsuario",
                table: "Usuario",
                column: "IdStatusUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_PapelUsuario_IdPapelUsuario",
                table: "Usuario",
                column: "IdPapelUsuario",
                principalTable: "PapelUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_StatusUsuario_IdStatusUsuario",
                table: "Usuario",
                column: "IdStatusUsuario",
                principalTable: "StatusUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_PapelUsuario_IdPapelUsuario",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_StatusUsuario_IdStatusUsuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdPapelUsuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdStatusUsuario",
                table: "Usuario");

            migrationBuilder.AddColumn<int>(
                name: "PapelUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PapelUsuarioId",
                table: "Usuario",
                column: "PapelUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_StatusUsuarioId",
                table: "Usuario",
                column: "StatusUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_PapelUsuario_PapelUsuarioId",
                table: "Usuario",
                column: "PapelUsuarioId",
                principalTable: "PapelUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_StatusUsuario_StatusUsuarioId",
                table: "Usuario",
                column: "StatusUsuarioId",
                principalTable: "StatusUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
