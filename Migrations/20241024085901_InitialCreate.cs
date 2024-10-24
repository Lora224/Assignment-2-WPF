using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_2_WPF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_pet_PetId",
                table: "activity");

            migrationBuilder.AddForeignKey(
                name: "FK_activity_pet_PetId",
                table: "activity",
                column: "PetId",
                principalTable: "pet",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_pet_PetId",
                table: "activity");

            migrationBuilder.AddForeignKey(
                name: "FK_activity_pet_PetId",
                table: "activity",
                column: "PetId",
                principalTable: "pet",
                principalColumn: "PetId");
        }
    }
}
