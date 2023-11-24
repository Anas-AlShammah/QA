using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.Migrations
{
    /// <inheritdoc />
    public partial class cat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Question",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8ec4314-7d66-427e-9210-2a421931025c", "AQAAAAIAAYagAAAAECORIcpYgv2BKxBRiqD0/3WGoVEZPOWuTz1xOyvafJW5+eMxBckIZLMF05B4Gea25A==", "6dab878e-2185-4c99-ab39-20f3d72dcff5" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_CategoryId",
                table: "Question",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Question_CategoryId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Question");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35499581-bafe-4294-aa76-f9cb270beb6b", "AQAAAAIAAYagAAAAEAi3VMrl1l2sMtS1UehAHyNU36AWsVySRHDv1HfNqMPCTWNnBRIzPmvbSArgmqz8sA==", "ade1e712-1a47-469b-8494-039dddbdb3bf" });
        }
    }
}
