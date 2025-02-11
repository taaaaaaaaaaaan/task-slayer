using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task_slayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Dificuldade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Dificuldade",
                table: "Tarefas",
                type: "smallint",
                maxLength: 20,
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dificuldade",
                table: "Tarefas");
        }
    }
}
