using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabClothingCollectionAPI.Migrations
{
    public partial class MoreChangesYet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Budget",
                table: "Collections",
                type: "float",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "Collections",
                type: "decimal(18,2)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 20);
        }
    }
}
