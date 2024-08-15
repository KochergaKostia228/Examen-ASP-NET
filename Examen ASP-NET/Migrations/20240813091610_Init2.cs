using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_ASP_NET.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Answers_AnswerId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Question_QuestionId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Results_ResultId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Results_ResultId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ResultId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tests_QuestionId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_ResultId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "Question",
                newName: "TestId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_AnswerId",
                table: "Question",
                newName: "IX_Question_TestId");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Results",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Results",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_TestId",
                table: "Results",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserId",
                table: "Results",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Tests_TestId",
                table: "Results",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Users_UserId",
                table: "Results",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Tests_TestId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Users_UserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_TestId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_UserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "Question",
                newName: "AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_TestId",
                table: "Question",
                newName: "IX_Question_AnswerId");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Tests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Tests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ResultId",
                table: "Users",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_QuestionId",
                table: "Tests",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ResultId",
                table: "Tests",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Answers_AnswerId",
                table: "Question",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Question_QuestionId",
                table: "Tests",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Results_ResultId",
                table: "Tests",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Results_ResultId",
                table: "Users",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id");
        }
    }
}
