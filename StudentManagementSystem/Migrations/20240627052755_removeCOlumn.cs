using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class removeCOlumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_studentClasses_ClassID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClassID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClassID",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassID",
                table: "AspNetUsers",
                column: "ClassID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_studentClasses_ClassID",
                table: "AspNetUsers",
                column: "ClassID",
                principalTable: "studentClasses",
                principalColumn: "StudentClassId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
