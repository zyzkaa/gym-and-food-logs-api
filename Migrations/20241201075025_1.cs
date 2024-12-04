using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mets",
                columns: table => new
                {
                    CardioExerciseId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MetValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mets", x => new { x.CardioExerciseId, x.StartSpeed });
                    table.ForeignKey(
                        name: "FK_Mets_CardioExercises_CardioExerciseId",
                        column: x => x.CardioExerciseId,
                        principalTable: "CardioExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mets");
        }
    }
}
