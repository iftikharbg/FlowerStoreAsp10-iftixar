using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowerStore.Migrations
{
    public partial class Sliderimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "SliderImage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SliderImage_SliderId",
                table: "SliderImage",
                column: "SliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SliderImage_Sliders_SliderId",
                table: "SliderImage",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderImage_Sliders_SliderId",
                table: "SliderImage");

            migrationBuilder.DropIndex(
                name: "IX_SliderImage_SliderId",
                table: "SliderImage");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "SliderImage");
        }
    }
}
