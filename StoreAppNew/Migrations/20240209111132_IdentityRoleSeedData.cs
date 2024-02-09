using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreAppNew2.Migrations
{
    public partial class IdentityRoleSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01c0a5a6-0ce4-4579-bbe5-7fbe1ad3900e", "9c3b3081-8b44-48df-a4fa-08d1ce547996", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4293c4c9-bc21-4313-bf80-c3121aad6604", "2c13f798-ca55-4325-bf36-747aacf21662", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bbf737c2-4ea0-4110-a9e2-699d11e0baa1", "ea09b4a8-f026-44cd-b05d-70fe437d5295", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01c0a5a6-0ce4-4579-bbe5-7fbe1ad3900e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4293c4c9-bc21-4313-bf80-c3121aad6604");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbf737c2-4ea0-4110-a9e2-699d11e0baa1");
        }
    }
}
