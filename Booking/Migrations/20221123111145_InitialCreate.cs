using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    OfficeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsSignedIn = table.Column<bool>(type: "boolean", nullable: false),
                    OfficeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsOccupied = table.Column<bool>(type: "boolean", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatUser",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatUser", x => new { x.SeatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SeatUser_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { 1, 20, "Lille Grensen" });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Capacity", "Name", "OfficeId" },
                values: new object[,]
                {
                    { 1, 10, "Storerommet", 1 },
                    { 2, 5, "Lillerommet", 1 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "IsSignedIn", "Name", "OfficeId" },
                values: new object[,]
                {
                    { 1, true, "Ted Mosby", 1 },
                    { 2, true, "Marshall Eriksen", 1 },
                    { 3, true, "Lily Aldrin", 1 },
                    { 4, false, "Barney Stinson", 1 },
                    { 5, false, "Robin Scherbatsky", 1 }
                });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "IsOccupied", "Name", "RoomId" },
                values: new object[,]
                {
                    { 1, true, "01", 1 },
                    { 2, false, "02", 1 },
                    { 3, false, "03", 1 },
                    { 4, false, "04", 1 },
                    { 5, false, "05", 1 },
                    { 6, false, "06", 1 },
                    { 7, false, "07", 1 },
                    { 8, false, "01", 1 },
                    { 9, false, "01", 1 },
                    { 10, false, "10", 1 },
                    { 11, false, "11", 2 },
                    { 12, false, "12", 2 },
                    { 13, false, "13", 2 },
                    { 14, false, "14", 2 },
                    { 15, false, "15", 2 }
                });

            migrationBuilder.InsertData(
                table: "SeatUser",
                columns: new[] { "SeatId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Room_OfficeId",
                table: "Room",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_RoomId",
                table: "Seat",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatUser_SeatId",
                table: "SeatUser",
                column: "SeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatUser_UserId",
                table: "SeatUser",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_OfficeId",
                table: "User",
                column: "OfficeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatUser");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Office");
        }
    }
}
