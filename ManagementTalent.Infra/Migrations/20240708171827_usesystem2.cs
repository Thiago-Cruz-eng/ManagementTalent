using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementTalent.Infra.Migrations
{
    /// <inheritdoc />
    public partial class usesystem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Creat_at",
                table: "UserSystems",
                newName: "CreateAt");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "UserSystems",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ColabId",
                table: "UserSystems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UserSystems");

            migrationBuilder.DropColumn(
                name: "ColabId",
                table: "UserSystems");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "UserSystems",
                newName: "Creat_at");
        }
    }
}
