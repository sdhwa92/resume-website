using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeSite.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvertEmploymentTypeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Experiences");

            migrationBuilder.AlterColumn<int>(
                name: "EmploymentType",
                table: "Experiences",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmploymentType",
                table: "Experiences",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Experiences",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
