using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AutoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardioExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardioExercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Muscles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrengthExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrengthExercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MuscleStrengthExercise",
                columns: table => new
                {
                    MusclesId = table.Column<int>(type: "INTEGER", nullable: false),
                    StrengthExercisesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleStrengthExercise", x => new { x.MusclesId, x.StrengthExercisesId });
                    table.ForeignKey(
                        name: "FK_MuscleStrengthExercise_Muscles_MusclesId",
                        column: x => x.MusclesId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuscleStrengthExercise_StrengthExercises_StrengthExercisesId",
                        column: x => x.StrengthExercisesId,
                        principalTable: "StrengthExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarExerciseInTrainings",
                columns: table => new
                {
                    TrainingId = table.Column<int>(type: "INTEGER", nullable: false),
                    CardioExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarExerciseInTrainings", x => new { x.CardioExerciseId, x.TrainingId });
                    table.ForeignKey(
                        name: "FK_CarExerciseInTrainings_CardioExercises_CardioExerciseId",
                        column: x => x.CardioExerciseId,
                        principalTable: "CardioExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarExerciseInTrainings_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrExerciseInTrainings",
                columns: table => new
                {
                    TrainingId = table.Column<int>(type: "INTEGER", nullable: false),
                    StrengthExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrExerciseInTrainings", x => new { x.StrengthExerciseId, x.TrainingId });
                    table.ForeignKey(
                        name: "FK_StrExerciseInTrainings_StrengthExercises_StrengthExerciseId",
                        column: x => x.StrengthExerciseId,
                        principalTable: "StrengthExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrExerciseInTrainings_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarExerciseParameters",
                columns: table => new
                {
                    Interval = table.Column<int>(type: "INTEGER", nullable: false),
                    CarExerciseInTrainingCardioExerciseId = table.Column<int>(type: "INTEGER", nullable: false),
                    CarExerciseInTrainingTrainingId = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false),
                    Seconds = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarExerciseParameters", x => new { x.Interval, x.CarExerciseInTrainingTrainingId, x.CarExerciseInTrainingCardioExerciseId });
                    table.ForeignKey(
                        name: "FK_CarExerciseParameters_CarExerciseInTrainings_CarExerciseInTrainingTrainingId_CarExerciseInTrainingCardioExerciseId",
                        columns: x => new { x.CarExerciseInTrainingTrainingId, x.CarExerciseInTrainingCardioExerciseId },
                        principalTable: "CarExerciseInTrainings",
                        principalColumns: new[] { "CardioExerciseId", "TrainingId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrExerciseParameters",
                columns: table => new
                {
                    Set = table.Column<int>(type: "INTEGER", nullable: false),
                    StrExerciseInTrainingStrengthExerciseId = table.Column<int>(type: "INTEGER", nullable: false),
                    StrExerciseInTrainingTrainingId = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<int>(type: "INTEGER", nullable: false),
                    Repetitions = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrExerciseParameters", x => new { x.Set, x.StrExerciseInTrainingTrainingId, x.StrExerciseInTrainingStrengthExerciseId });
                    table.ForeignKey(
                        name: "FK_StrExerciseParameters_StrExerciseInTrainings_StrExerciseInTrainingTrainingId_StrExerciseInTrainingStrengthExerciseId",
                        columns: x => new { x.StrExerciseInTrainingTrainingId, x.StrExerciseInTrainingStrengthExerciseId },
                        principalTable: "StrExerciseInTrainings",
                        principalColumns: new[] { "StrengthExerciseId", "TrainingId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarExerciseInTrainings_TrainingId",
                table: "CarExerciseInTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_CarExerciseParameters_CarExerciseInTrainingTrainingId_CarExerciseInTrainingCardioExerciseId",
                table: "CarExerciseParameters",
                columns: new[] { "CarExerciseInTrainingTrainingId", "CarExerciseInTrainingCardioExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_MuscleStrengthExercise_StrengthExercisesId",
                table: "MuscleStrengthExercise",
                column: "StrengthExercisesId");

            migrationBuilder.CreateIndex(
                name: "IX_StrExerciseInTrainings_TrainingId",
                table: "StrExerciseInTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_StrExerciseParameters_StrExerciseInTrainingTrainingId_StrExerciseInTrainingStrengthExerciseId",
                table: "StrExerciseParameters",
                columns: new[] { "StrExerciseInTrainingTrainingId", "StrExerciseInTrainingStrengthExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_UserId",
                table: "Trainings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarExerciseParameters");

            migrationBuilder.DropTable(
                name: "MuscleStrengthExercise");

            migrationBuilder.DropTable(
                name: "StrExerciseParameters");

            migrationBuilder.DropTable(
                name: "CarExerciseInTrainings");

            migrationBuilder.DropTable(
                name: "Muscles");

            migrationBuilder.DropTable(
                name: "StrExerciseInTrainings");

            migrationBuilder.DropTable(
                name: "CardioExercises");

            migrationBuilder.DropTable(
                name: "StrengthExercises");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
