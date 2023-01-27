using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
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
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
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
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_User_UserId",
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
                table: "User",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "1", "ted.mosby@himym.com", "Ted Mosby", "44112233" },
                    { "10", "joey.tribiani@friends.com", "Joey Tribiani", "99556677" },
                    { "11", "rachel.green@friends.com", "Rachel Green", "99667788" },
                    { "12", "test.user@testing.com", "Test User", "98765432" },
                    { "2", "marshall.eriksen@himym.com", "Marshall Eriksen", "44223344" },
                    { "3", "lily.aldrin@himym.com", "Lily Aldrin", "44334455" },
                    { "4", "barney.stinson@himym.com", "Barney Stinson", "44445566" },
                    { "5", "robin.scherbatsky@himym.com", "Robin Scherbatsky", "44556677" },
                    { "6", "ross.geller@friends.com", "Ross Geller", "99112233" },
                    { "7", "monica.geller@friends.com", "Monica Geller", "99223344" },
                    { "8", "phoebe.buffay@friends.com", "Phoebe Buffay", "99334455" },
                    { "9", "chandler.bing@friends.com", "Chandler Bing", "99445566" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Capacity", "Name", "OfficeId" },
                values: new object[,]
                {
                    { 1, 10, "Storerommet", 1 },
                    { 2, 5, "Lillerommet", 1 }
                });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "Name", "RoomId" },
                values: new object[,]
                {
                    { 1, "01", 1 },
                    { 2, "02", 1 },
                    { 3, "03", 1 },
                    { 4, "04", 1 },
                    { 5, "05", 1 },
                    { 6, "06", 1 },
                    { 7, "07", 1 },
                    { 8, "01", 1 },
                    { 9, "01", 1 },
                    { 10, "10", 1 },
                    { 11, "11", 2 },
                    { 12, "12", 2 },
                    { 13, "13", 2 },
                    { 14, "14", 2 },
                    { 15, "15", 2 }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "Id", "Date", "SeatId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 2, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 3, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 4, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 5, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 6, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 7, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 8, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 9, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 10, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 11, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 12, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 13, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 14, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 15, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 16, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 17, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 18, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 19, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 20, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 11, "1" },
                    { 21, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 22, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 23, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 24, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 25, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 26, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 27, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 28, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 29, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 30, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 31, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 32, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 33, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 34, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 35, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 36, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 37, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 38, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 39, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 40, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 12, "2" },
                    { 41, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 42, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 43, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 44, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 45, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 46, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 47, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 48, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 49, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 50, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 51, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 52, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 53, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 54, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 55, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 56, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 57, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 58, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 59, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 60, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 13, "3" },
                    { 61, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 62, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 63, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 64, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 65, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 66, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 67, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 68, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 69, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 70, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 71, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 72, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 73, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 74, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 75, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 76, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 77, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 78, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 79, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 80, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 14, "4" },
                    { 81, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 82, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 83, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 84, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 85, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 86, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 87, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 88, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 89, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 90, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 91, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 92, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 93, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 94, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 95, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 96, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 97, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 98, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 99, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 100, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 15, "5" },
                    { 101, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 102, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 103, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 104, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 105, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 106, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 107, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 108, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 109, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 110, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 111, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 112, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 113, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 114, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 115, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 116, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 117, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 118, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 119, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 120, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 1, "6" },
                    { 121, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 122, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 123, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 124, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 125, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 126, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 127, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 128, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 129, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 130, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 131, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 132, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 133, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 134, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 135, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 136, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 137, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 138, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 139, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 140, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 2, "7" },
                    { 141, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 142, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 143, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 144, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 145, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 146, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 147, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 148, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 149, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 150, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 151, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 152, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 153, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 154, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 155, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 156, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 157, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 158, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 159, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 160, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 3, "8" },
                    { 161, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 162, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 163, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 164, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 165, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 166, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 167, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 168, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 169, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 170, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 171, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 172, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 173, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 174, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 175, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 176, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 177, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 178, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 179, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 180, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 4, "9" },
                    { 181, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 182, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 183, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 184, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 185, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 186, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 187, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 188, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 189, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 190, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 191, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 192, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 193, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 194, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 195, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 196, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 197, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 198, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 199, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 200, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 5, "10" },
                    { 201, new DateTime(2023, 1, 25, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 202, new DateTime(2023, 1, 26, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 203, new DateTime(2023, 1, 27, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 204, new DateTime(2023, 1, 28, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 205, new DateTime(2023, 1, 29, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 206, new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 207, new DateTime(2023, 1, 31, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 208, new DateTime(2023, 2, 1, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 209, new DateTime(2023, 2, 2, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 210, new DateTime(2023, 2, 3, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 211, new DateTime(2023, 2, 4, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 212, new DateTime(2023, 2, 5, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 213, new DateTime(2023, 2, 6, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 214, new DateTime(2023, 2, 7, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 215, new DateTime(2023, 2, 8, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 216, new DateTime(2023, 2, 9, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 217, new DateTime(2023, 2, 10, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 218, new DateTime(2023, 2, 11, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 219, new DateTime(2023, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" },
                    { 220, new DateTime(2023, 2, 13, 23, 0, 0, 0, DateTimeKind.Utc), 6, "11" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SeatId",
                table: "Booking",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId",
                table: "Booking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_OfficeId",
                table: "Room",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_RoomId",
                table: "Seat",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

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
