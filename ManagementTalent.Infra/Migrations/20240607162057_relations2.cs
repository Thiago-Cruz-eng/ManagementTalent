using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementTalent.Infra.Migrations
{
    /// <inheritdoc />
    public partial class relations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reality",
                table: "AssessmentParamResults",
                newName: "RealityResult");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RealityResult",
                table: "AssessmentParamResults",
                newName: "Reality");
        }
    }
}
