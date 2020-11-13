using Microsoft.EntityFrameworkCore.Migrations;

namespace Raketti.Server.Migrations
{
    public partial class UserDisplayNameComputedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "usersORM",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "(Trim(([FirstName]+' ')+isnull([LastName],'')))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "usersORM");
        }
    }
}
