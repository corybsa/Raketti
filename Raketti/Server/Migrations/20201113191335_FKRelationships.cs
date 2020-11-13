using Microsoft.EntityFrameworkCore.Migrations;

namespace Raketti.Server.Migrations
{
    public partial class FKRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QueueId",
                table: "userRolesORM",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "QueueAssignable",
                table: "userRolesORM",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_userRolesORM_UserId",
                table: "userRolesORM",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userRolesORM_usersORM_UserId",
                table: "userRolesORM",
                column: "UserId",
                principalTable: "usersORM",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRolesORM_usersORM_UserId",
                table: "userRolesORM");

            migrationBuilder.DropIndex(
                name: "IX_userRolesORM_UserId",
                table: "userRolesORM");

            migrationBuilder.AlterColumn<int>(
                name: "QueueId",
                table: "userRolesORM",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "QueueAssignable",
                table: "userRolesORM",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
