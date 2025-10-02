using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicFestival.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Performances_Festivals_FestivalId",
                table: "Performances");

            migrationBuilder.DropIndex(
                name: "IX_Performances_FestivalId",
                table: "Performances");

            migrationBuilder.DropColumn(
                name: "FestivalId",
                table: "Performances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FestivalId",
                table: "Performances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Performances_FestivalId",
                table: "Performances",
                column: "FestivalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Performances_Festivals_FestivalId",
                table: "Performances",
                column: "FestivalId",
                principalTable: "Festivals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
