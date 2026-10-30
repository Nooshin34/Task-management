using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplitUserFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""
                UPDATE Users SET
                    FirstName = CASE
                        WHEN CHARINDEX(N' ', FullName) > 0 THEN LEFT(FullName, CHARINDEX(N' ', FullName) - 1)
                        ELSE FullName
                    END,
                    LastName = CASE
                        WHEN CHARINDEX(N' ', FullName) > 0 THEN LTRIM(SUBSTRING(FullName, CHARINDEX(N' ', FullName) + 1, 150))
                        ELSE N''
                    END;
                """);

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""
                UPDATE Users SET FullName = LTRIM(RTRIM(FirstName + N' ' + LastName));
                """);

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");
        }
    }
}
