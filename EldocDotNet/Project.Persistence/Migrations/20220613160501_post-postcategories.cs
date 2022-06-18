using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistence.Migrations
{
    public partial class postpostcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uri",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostCategoryId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostCategoryId",
                table: "Posts",
                column: "PostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryId",
                table: "Posts",
                column: "PostCategoryId",
                principalTable: "PostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostCategoryId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostCategoryId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Uri",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
