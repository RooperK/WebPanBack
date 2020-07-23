using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.ContextSetup.Migrations
{
    public partial class InitialEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Panoramas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false),
                    Ath = table.Column<float>(nullable: false),
                    Aphi = table.Column<float>(nullable: false),
                    Bth = table.Column<float>(nullable: false),
                    Bphi = table.Column<float>(nullable: false),
                    PositionX = table.Column<float>(nullable: false),
                    PositionY = table.Column<float>(nullable: false),
                    PositionZ = table.Column<float>(nullable: false),
                    TileSizeX = table.Column<int>(nullable: false),
                    TileSizeY = table.Column<int>(nullable: false),
                    ImageHqWidth = table.Column<int>(nullable: false),
                    ImageHqHeight = table.Column<int>(nullable: false),
                    ImageLqWidth = table.Column<int>(nullable: false),
                    ImageLqHeight = table.Column<int>(nullable: false),
                    TourId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panoramas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Panoramas_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Markers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    PositionX = table.Column<float>(nullable: false),
                    PositionY = table.Column<float>(nullable: false),
                    PositionZ = table.Column<float>(nullable: false),
                    CurrentId = table.Column<string>(nullable: true),
                    NextId = table.Column<string>(nullable: true),
                    IsExternal = table.Column<bool>(nullable: false),
                    TourId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markers_Panoramas_CurrentId",
                        column: x => x.CurrentId,
                        principalTable: "Panoramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Markers_Panoramas_NextId",
                        column: x => x.NextId,
                        principalTable: "Panoramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Markers_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    IsHq = table.Column<bool>(nullable: false),
                    PanoramaId = table.Column<string>(nullable: true),
                    IsExternal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tiles_Panoramas_PanoramaId",
                        column: x => x.PanoramaId,
                        principalTable: "Panoramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Markers_CurrentId",
                table: "Markers",
                column: "CurrentId");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_NextId",
                table: "Markers",
                column: "NextId");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_TourId",
                table: "Markers",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Panoramas_TourId",
                table: "Panoramas",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tiles_PanoramaId",
                table: "Tiles",
                column: "PanoramaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markers");

            migrationBuilder.DropTable(
                name: "Tiles");

            migrationBuilder.DropTable(
                name: "Panoramas");

            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
