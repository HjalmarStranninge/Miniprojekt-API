using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miniprojekt_API.Migrations
{
    public partial class NameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestPerson_Persons_PersonsId",
                table: "InterestPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Persons_PersonId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "People");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Interests",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PersonsId",
                table: "InterestPerson",
                newName: "PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestPerson_PersonsId",
                table: "InterestPerson",
                newName: "IX_InterestPerson_PeopleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPerson_People_PeopleId",
                table: "InterestPerson",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_People_PersonId",
                table: "Links",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestPerson_People_PeopleId",
                table: "InterestPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_People_PersonId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Persons");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Interests",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "InterestPerson",
                newName: "PersonsId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestPerson_PeopleId",
                table: "InterestPerson",
                newName: "IX_InterestPerson_PersonsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPerson_Persons_PersonsId",
                table: "InterestPerson",
                column: "PersonsId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Persons_PersonId",
                table: "Links",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
