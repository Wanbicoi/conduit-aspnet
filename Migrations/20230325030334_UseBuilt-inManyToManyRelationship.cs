using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealWorld.Migrations
{
    /// <inheritdoc />
    public partial class UseBuiltinManyToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Articles_ArticleID",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "ArticleTag");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ArticleID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ArticleID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Favorited",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "FavoritesCount",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Articles",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ArticleFavorites",
                columns: table => new
                {
                    FavoriteArticlesId = table.Column<int>(type: "INTEGER", nullable: false),
                    FavoriteUsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleFavorites", x => new { x.FavoriteArticlesId, x.FavoriteUsersId });
                    table.ForeignKey(
                        name: "FK_ArticleFavorites_Articles_FavoriteArticlesId",
                        column: x => x.FavoriteArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleFavorites_Users_FavoriteUsersId",
                        column: x => x.FavoriteUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => new { x.ArticlesId, x.TagsName });
                    table.ForeignKey(
                        name: "FK_ArticleTags_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTags_Tags_TagsName",
                        column: x => x.TagsName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 220, 228, 130, 131, 167, 129, 194, 186, 39, 200, 224, 139, 154, 143, 200, 164, 97, 4, 202, 134, 195, 119, 45, 236, 93, 166, 232, 37, 95, 220, 36, 160, 159, 183, 74, 22, 40, 62, 177, 100, 233, 58, 171, 65, 108, 255, 16, 30, 47, 77, 63, 9, 66, 95, 250, 62, 61, 6, 177, 28, 38, 125, 251, 213 }, new byte[] { 220, 148, 217, 179, 184, 255, 22, 75, 135, 105, 21, 40, 161, 16, 164, 199 } });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFavorites_FavoriteUsersId",
                table: "ArticleFavorites",
                column: "FavoriteUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_TagsName",
                table: "ArticleTags",
                column: "TagsName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleFavorites");

            migrationBuilder.DropTable(
                name: "ArticleTags");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Articles",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "ArticleID",
                table: "Tags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Articles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Articles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "Favorited",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FavoritesCount",
                table: "Articles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                columns: table => new
                {
                    TagID = table.Column<string>(type: "TEXT", nullable: false),
                    ArticleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => new { x.TagID, x.ArticleID });
                    table.ForeignKey(
                        name: "FK_ArticleTag_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTag_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "ChauLenBa",
                column: "ArticleID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "HelloWorld",
                column: "ArticleID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "HoangTheTrung",
                column: "ArticleID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "hocChoGioi",
                column: "ArticleID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "LonRoiConHayKhocNhe",
                column: "ArticleID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "lsopaslkj",
                column: "ArticleID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { new byte[] { 63, 181, 104, 190, 154, 165, 29, 167, 240, 5, 241, 150, 210, 185, 22, 215, 181, 186, 52, 145, 26, 70, 225, 251, 202, 215, 80, 195, 63, 57, 69, 81, 224, 28, 181, 110, 102, 17, 37, 40, 43, 229, 167, 137, 225, 148, 246, 173, 24, 189, 196, 129, 222, 18, 233, 217, 103, 53, 28, 145, 8, 247, 99, 194 }, new byte[] { 23, 55, 63, 104, 110, 198, 205, 78, 173, 107, 24, 206, 197, 119, 112, 116 } });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ArticleID",
                table: "Tags",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_ArticleID",
                table: "ArticleTag",
                column: "ArticleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Articles_ArticleID",
                table: "Tags",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ID");
        }
    }
}
