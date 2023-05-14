using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TelephoneNetworkApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutomaticTelephoneExchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountSubscriber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomaticTelephoneExchange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecondName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsIntercityOpen = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtsSubscriber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberId = table.Column<int>(type: "int", nullable: true),
                    AutomaticTelephoneExchangeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtsSubscriber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtsSubscriber_AutomaticTelephoneExchange_AutomaticTelephoneExchangeId",
                        column: x => x.AutomaticTelephoneExchangeId,
                        principalTable: "AutomaticTelephoneExchange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AtsSubscriber_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RegistrySubscriptionPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mounth = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    Year = table.Column<int>(type: "int", nullable: false, defaultValue: 2023),
                    TownshipMinuteCount = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    IntecityMinuteCount = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubscriberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrySubscriptionPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrySubscriptionPayment_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AutomaticTelephoneExchange",
                columns: new[] { "Id", "CountSubscriber", "Name", "Town" },
                values: new object[] { 78, 10000, "Телефонная сеть №15", "Витебск" });

            migrationBuilder.InsertData(
                table: "Subscribers",
                columns: new[] { "Id", "Name", "PhoneNumber", "SecondName", "Surname" },
                values: new object[,]
                {
                    { 100, "Василий", "23-56-78", "Пупкин", "Петрович" },
                    { 101, "Иван", "13-56-78", "Иванов", "Иванович" }
                });

            migrationBuilder.InsertData(
                table: "AtsSubscriber",
                columns: new[] { "Id", "AutomaticTelephoneExchangeId", "SubscriberId" },
                values: new object[] { 100, 78, 101 });

            migrationBuilder.InsertData(
                table: "RegistrySubscriptionPayment",
                columns: new[] { "Id", "Price", "SubscriberId", "TownshipMinuteCount" },
                values: new object[,]
                {
                    { 99, 0.25m, 101, (byte)5 },
                    { 100, 0.35m, 100, (byte)7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtsSubscriber_AutomaticTelephoneExchangeId",
                table: "AtsSubscriber",
                column: "AutomaticTelephoneExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_AtsSubscriber_SubscriberId",
                table: "AtsSubscriber",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrySubscriptionPayment_SubscriberId",
                table: "RegistrySubscriptionPayment",
                column: "SubscriberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtsSubscriber");

            migrationBuilder.DropTable(
                name: "RegistrySubscriptionPayment");

            migrationBuilder.DropTable(
                name: "AutomaticTelephoneExchange");

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
