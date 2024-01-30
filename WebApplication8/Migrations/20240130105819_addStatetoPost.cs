using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class addStatetoPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31ce0a42-2773-4bc3-b545-15039ba2fbde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d36078af-1807-42e8-a503-a5ba780f52f0");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "273f87f9-8cc4-4c45-8be9-0b63371a86e4", "2", "User", "User" },
                    { "e4bb3693-dd65-4a50-8074-5830176cfa0b", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "273f87f9-8cc4-4c45-8be9-0b63371a86e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4bb3693-dd65-4a50-8074-5830176cfa0b");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Post");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31ce0a42-2773-4bc3-b545-15039ba2fbde", "2", "User", "User" },
                    { "d36078af-1807-42e8-a503-a5ba780f52f0", "1", "Admin", "Admin" }
                });
        }
    }
}
