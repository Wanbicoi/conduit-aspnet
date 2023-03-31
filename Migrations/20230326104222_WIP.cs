using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealWorld.Migrations
{
    /// <inheritdoc />
    public partial class WIP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserFollowers",
                columns: table => new
                {
                    FollowersId = table.Column<int>(type: "INTEGER", nullable: false),
                    FollowingsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowers", x => new { x.FollowersId, x.FollowingsId });
                    table.ForeignKey(
                        name: "FK_UserFollowers_Users_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFollowers_Users_FollowingsId",
                        column: x => x.FollowingsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 131, 170, 69, 180, 0, 238, 23, 26, 141, 135, 47, 241, 246, 194, 129, 105, 21, 207, 106, 8, 189, 146, 233, 41, 140, 203, 118, 79, 61, 83, 168, 131, 82, 94, 124, 173, 185, 155, 155, 138, 19, 2, 129, 107, 247, 243, 114, 56, 61, 173, 1, 198, 176, 71, 231, 40, 53, 189, 102, 31, 81, 16, 180, 42 }, new byte[] { 78, 223, 120, 38, 96, 189, 215, 67, 166, 14, 29, 19, 36, 207, 119, 191 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "Email", "Hash", "Image", "Salt", "UserName" },
                values: new object[] { 2, "", "wan@gmail.com", new byte[] { 131, 170, 69, 180, 0, 238, 23, 26, 141, 135, 47, 241, 246, 194, 129, 105, 21, 207, 106, 8, 189, 146, 233, 41, 140, 203, 118, 79, 61, 83, 168, 131, 82, 94, 124, 173, 185, 155, 155, 138, 19, 2, 129, 107, 247, 243, 114, 56, 61, 173, 1, 198, 176, 71, 231, 40, 53, 189, 102, 31, 81, 16, 180, 42 }, "", new byte[] { 78, 223, 120, 38, 96, 189, 215, 67, 166, 14, 29, 19, 36, 207, 119, 191 }, "SongGa" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowingsId",
                table: "UserFollowers",
                column: "FollowingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "UserFollowers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Articles",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 220, 228, 130, 131, 167, 129, 194, 186, 39, 200, 224, 139, 154, 143, 200, 164, 97, 4, 202, 134, 195, 119, 45, 236, 93, 166, 232, 37, 95, 220, 36, 160, 159, 183, 74, 22, 40, 62, 177, 100, 233, 58, 171, 65, 108, 255, 16, 30, 47, 77, 63, 9, 66, 95, 250, 62, 61, 6, 177, 28, 38, 125, 251, 213 }, new byte[] { 220, 148, 217, 179, 184, 255, 22, 75, 135, 105, 21, 40, 161, 16, 164, 199 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
