using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagmentSystemApiProject.Migrations
{
    /// <inheritdoc />
    public partial class Security : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.AlterColumn<byte[]>(
                 name: "PasswordHash",
                 table: "Users",
                 type: "varbinary(max)",
                 nullable: false,
                 oldClrType: typeof(string),
                 oldType: "nvarchar(max)");*/

            migrationBuilder.AddColumn<byte[]>(
            name: "PasswordHashTemp",
            table: "Users",
            type: "varbinary(max)",
            nullable: true);

            migrationBuilder.Sql(
            "UPDATE Users SET PasswordHashTemp = CAST(PasswordHash AS varbinary(max))");

            migrationBuilder.DropColumn(
            name: "PasswordHash",
            table: "Users");

            migrationBuilder.RenameColumn(
            name: "PasswordHashTemp",
            table: "Users",
            newName: "PasswordHash");


            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
