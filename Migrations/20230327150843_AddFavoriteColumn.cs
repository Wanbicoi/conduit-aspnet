using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealWorld.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorited",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 186, 32, 9, 108, 50, 93, 88, 61, 155, 252, 67, 12, 187, 74, 110, 180, 31, 113, 217, 108, 56, 134, 11, 244, 33, 236, 70, 232, 56, 109, 227, 105, 9, 189, 99, 75, 41, 76, 92, 216, 65, 31, 243, 162, 7, 171, 60, 63, 114, 160, 101, 2, 34, 85, 83, 20, 179, 130, 53, 119, 25, 244, 67, 42 }, new byte[] { 107, 48, 110, 250, 161, 188, 212, 70, 149, 2, 249, 103, 1, 115, 42, 11 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 186, 32, 9, 108, 50, 93, 88, 61, 155, 252, 67, 12, 187, 74, 110, 180, 31, 113, 217, 108, 56, 134, 11, 244, 33, 236, 70, 232, 56, 109, 227, 105, 9, 189, 99, 75, 41, 76, 92, 216, 65, 31, 243, 162, 7, 171, 60, 63, 114, 160, 101, 2, 34, 85, 83, 20, 179, 130, 53, 119, 25, 244, 67, 42 }, new byte[] { 107, 48, 110, 250, 161, 188, 212, 70, 149, 2, 249, 103, 1, 115, 42, 11 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorited",
                table: "Articles");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 131, 170, 69, 180, 0, 238, 23, 26, 141, 135, 47, 241, 246, 194, 129, 105, 21, 207, 106, 8, 189, 146, 233, 41, 140, 203, 118, 79, 61, 83, 168, 131, 82, 94, 124, 173, 185, 155, 155, 138, 19, 2, 129, 107, 247, 243, 114, 56, 61, 173, 1, 198, 176, 71, 231, 40, 53, 189, 102, 31, 81, 16, 180, 42 }, new byte[] { 78, 223, 120, 38, 96, 189, 215, 67, 166, 14, 29, 19, 36, 207, 119, 191 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 131, 170, 69, 180, 0, 238, 23, 26, 141, 135, 47, 241, 246, 194, 129, 105, 21, 207, 106, 8, 189, 146, 233, 41, 140, 203, 118, 79, 61, 83, 168, 131, 82, 94, 124, 173, 185, 155, 155, 138, 19, 2, 129, 107, 247, 243, 114, 56, 61, 173, 1, 198, 176, 71, 231, 40, 53, 189, 102, 31, 81, 16, 180, 42 }, new byte[] { 78, 223, 120, 38, 96, 189, 215, 67, 166, 14, 29, 19, 36, 207, 119, 191 } });
        }
    }
}
