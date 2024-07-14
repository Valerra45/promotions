using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "managers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_managers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partner",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_partner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pictures",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mongo_id = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pictures", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "goods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    picture_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_goods", x => x.id);
                    table.ForeignKey(
                        name: "fk_goods_pictures_picture_id",
                        column: x => x.picture_id,
                        principalTable: "pictures",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    logo_picture_id = table.Column<Guid>(type: "uuid", nullable: true),
                    header = table.Column<string>(type: "text", nullable: false),
                    conditions = table.Column<string>(type: "text", nullable: false),
                    special_conditions = table.Column<string>(type: "text", nullable: false),
                    special_conditions2 = table.Column<string>(type: "text", nullable: false),
                    basement = table.Column<string>(type: "text", nullable: false),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enable = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotions", x => x.id);
                    table.ForeignKey(
                        name: "fk_promotions_pictures_logo_picture_id",
                        column: x => x.logo_picture_id,
                        principalTable: "pictures",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    partner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_partner_partner_id",
                        column: x => x.partner_id,
                        principalTable: "partner",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_promotions_promotion_id",
                        column: x => x.promotion_id,
                        principalTable: "promotions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promotion_goods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    goods_id = table.Column<Guid>(type: "uuid", nullable: true),
                    vendor_code = table.Column<string>(type: "text", nullable: false),
                    goods_description = table.Column<string>(type: "text", nullable: false),
                    promotion_description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotion_goods", x => x.id);
                    table.ForeignKey(
                        name: "fk_promotion_goods_goods_goods_id",
                        column: x => x.goods_id,
                        principalTable: "goods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_promotion_goods_promotions_promotion_id",
                        column: x => x.promotion_id,
                        principalTable: "promotions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "send_promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    partner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    manager_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_send_promotions", x => x.id);
                    table.ForeignKey(
                        name: "fk_send_promotions_managers_manager_id",
                        column: x => x.manager_id,
                        principalTable: "managers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_send_promotions_partner_partner_id",
                        column: x => x.partner_id,
                        principalTable: "partner",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_send_promotions_promotions_promotion_id",
                        column: x => x.promotion_id,
                        principalTable: "promotions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_goods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    goods_id = table.Column<Guid>(type: "uuid", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    count = table.Column<decimal>(type: "numeric", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_goods", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_goods_goods_goods_id",
                        column: x => x.goods_id,
                        principalTable: "goods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_order_goods_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_goods_picture_id",
                table: "goods",
                column: "picture_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_goods_goods_id",
                table: "order_goods",
                column: "goods_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_goods_order_id",
                table: "order_goods",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_partner_id",
                table: "orders",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_promotion_id",
                table: "orders",
                column: "promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_promotion_goods_goods_id",
                table: "promotion_goods",
                column: "goods_id");

            migrationBuilder.CreateIndex(
                name: "ix_promotion_goods_promotion_id",
                table: "promotion_goods",
                column: "promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_promotions_logo_picture_id",
                table: "promotions",
                column: "logo_picture_id");

            migrationBuilder.CreateIndex(
                name: "ix_send_promotions_manager_id",
                table: "send_promotions",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "ix_send_promotions_partner_id",
                table: "send_promotions",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "ix_send_promotions_promotion_id",
                table: "send_promotions",
                column: "promotion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_goods");

            migrationBuilder.DropTable(
                name: "promotion_goods");

            migrationBuilder.DropTable(
                name: "send_promotions");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "goods");

            migrationBuilder.DropTable(
                name: "managers");

            migrationBuilder.DropTable(
                name: "partner");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "pictures");
        }
    }
}
