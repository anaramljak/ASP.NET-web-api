using Microsoft.EntityFrameworkCore.Migrations;
using System;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReviewerAPI.Migrations
{
    public partial class ReviewSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Reviews",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   Message = table.Column<string>(nullable: true),
                   Created = table.Column<DateTime>(nullable: false),
                   PostId = table.Column<int>(nullable: true),
                   Rating = table.Column<int>(nullable: true),
                   UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)

               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Reviews", x => x.Id);
                   table.ForeignKey(
                       name: "FK_Reviews_Posts_PostId",
                       column: x => x.PostId,
                       principalTable: "Posts",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                         name: "FK_Reviews_Users_UserId",
                         column: x => x.UserId,
                         principalTable: "AspNetUsers",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Restrict);
               });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    ReviewId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                         name: "FK_Reviews_Users_UserId",
                         column: x => x.UserId,
                         principalTable: "AspNetUsers",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Restrict); 

                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PostId",
                table: "Reviews",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReviewId",
                table: "Comments",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
