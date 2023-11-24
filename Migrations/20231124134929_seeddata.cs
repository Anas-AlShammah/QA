using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QA.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d2bc124-d787-4cd5-b87e-36857395ba39", "AQAAAAIAAYagAAAAEJN7cEpT1rbG45cGDDkLAjN5YXmJ4i1YpXPjVFdrHf+k0WWB6fv7FPyJgWIVsj3aOQ==", "1068c1bb-1a0b-4677-8e00-59aa97c7ab1c" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "المعادن والفيتامينات" },
                    { 2, "المكملات الغذائيه" },
                    { 3, "الاعشاب" },
                    { 4, "الجهاز الهضمي" },
                    { 5, "الجهاز العصبي" },
                    { 6, "أنظمة غذائية" },
                    { 7, "أمراض النسائية" },
                    { 8, "امراض الذكورة" },
                    { 9, "الغدد" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8ec4314-7d66-427e-9210-2a421931025c", "AQAAAAIAAYagAAAAECORIcpYgv2BKxBRiqD0/3WGoVEZPOWuTz1xOyvafJW5+eMxBckIZLMF05B4Gea25A==", "6dab878e-2185-4c99-ab39-20f3d72dcff5" });
        }
    }
}
