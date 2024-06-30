using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentClassId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentClassId",
                table: "AspNetUsers",
                column: "StudentClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_studentClasses_StudentClassId",
                table: "AspNetUsers",
                column: "StudentClassId",
                principalTable: "studentClasses",
                principalColumn: "StudentClassId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_studentClasses_StudentClassId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentClassId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentClassId",
                table: "AspNetUsers");
        }
    }
}
