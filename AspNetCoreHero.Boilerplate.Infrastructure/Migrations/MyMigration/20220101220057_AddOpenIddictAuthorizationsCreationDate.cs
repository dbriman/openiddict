using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuthorizationServer.Migrations
{
    public partial class AddOpenIddictAuthorizationsCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
             name: "CreationDate",
             schema: "Identity",
             table: "OpenIddictAuthorizations",
             nullable: true);
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
