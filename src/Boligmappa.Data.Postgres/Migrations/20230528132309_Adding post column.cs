using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boligmappa.Data.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Addingpostcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMorethanTwoReactions",
                table: "Posts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMorethanTwoReactions",
                table: "Posts");
        }
    }
}
