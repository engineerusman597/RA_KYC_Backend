using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RA_KYC_BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsApproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Identity.User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Identity.User");
        }
    }
}
