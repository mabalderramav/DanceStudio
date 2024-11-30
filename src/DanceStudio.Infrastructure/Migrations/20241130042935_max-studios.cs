using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanceStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class maxstudios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxStudioCount",
                table: "Subscription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudioIds",
                table: "Subscription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxStudioCount",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "StudioIds",
                table: "Subscription");
        }
    }
}
