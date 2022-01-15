using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuthorizationServer.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(schema: "Identity",
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(schema: "Identity",
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(schema: "Identity",
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(schema: "Identity",
                name: "OpenIddictApplications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
