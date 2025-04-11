using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class ingredientWeightToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "Ingredients",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "Weight of the ingredient",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Weight of the ingredient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Ingredients",
                type: "int",
                nullable: false,
                comment: "Weight of the ingredient",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "Weight of the ingredient");
        }
    }
}
