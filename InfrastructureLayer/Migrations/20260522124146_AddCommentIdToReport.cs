using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentIdToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reports_AccountId_AdvertisementId",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AccountId_AdvertisementId_CommentId",
                table: "Reports",
                columns: new[] { "AccountId", "AdvertisementId", "CommentId" },
                unique: true,
                filter: "[CommentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reports_AccountId_AdvertisementId_CommentId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Reports");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AccountId_AdvertisementId",
                table: "Reports",
                columns: new[] { "AccountId", "AdvertisementId" },
                unique: true);
        }
    }
}
