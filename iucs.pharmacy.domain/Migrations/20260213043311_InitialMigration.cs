using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iucs.pharmacy.domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alert",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    alert_type = table.Column<int>(type: "integer", nullable: false),
                    reference_id = table.Column<Guid>(type: "uuid", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_alert", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "controlled_drug_register",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sales_invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    doctor_name = table.Column<string>(type: "text", nullable: false),
                    doctor_registration_no = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    performed_by = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_controlled_drug_register", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    customer_code = table.Column<string>(type: "text", nullable: true),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    mobile = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    manufacturer_name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_manufacturer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicine_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    category_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medicine_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sales_return",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sales_invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    return_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: true),
                    approved_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales_return", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock_adjustment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    adjustment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: true),
                    approved_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_adjustment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock_damage_expiry",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    reason = table.Column<int>(type: "integer", nullable: false),
                    recorded_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    approved_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_damage_expiry", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock_transfer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    from_branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    transfer_no = table.Column<string>(type: "text", nullable: false),
                    transfer_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_transfer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    supplier_code = table.Column<string>(type: "text", nullable: false),
                    supplier_name = table.Column<string>(type: "text", nullable: false),
                    contact_person = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    gstin = table.Column<string>(type: "text", nullable: true),
                    drug_license_no = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prescription",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    doctor_name = table.Column<string>(type: "text", nullable: false),
                    doctor_registration_no = table.Column<string>(type: "text", nullable: false),
                    hospital_name = table.Column<string>(type: "text", nullable: true),
                    prescription_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    prescription_image_path = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prescription", x => x.id);
                    table.ForeignKey(
                        name: "fk_prescription_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicine",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    manufacturer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_code = table.Column<string>(type: "text", nullable: false),
                    medicine_name = table.Column<string>(type: "text", nullable: false),
                    generic_name = table.Column<string>(type: "text", nullable: true),
                    dosage_form = table.Column<string>(type: "text", nullable: false),
                    strength = table.Column<string>(type: "text", nullable: true),
                    pack_size = table.Column<string>(type: "text", nullable: true),
                    hsn_code = table.Column<string>(type: "text", nullable: true),
                    is_prescription_required = table.Column<bool>(type: "boolean", nullable: false),
                    is_controlled = table.Column<bool>(type: "boolean", nullable: false),
                    default_gst_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    schedule_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medicine", x => x.id);
                    table.ForeignKey(
                        name: "fk_medicine_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_medicine_medicine_category_category_id",
                        column: x => x.category_id,
                        principalTable: "medicine_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_return_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    sales_return_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    refund_amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales_return_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_return_item_sales_return_sales_return_id",
                        column: x => x.sales_return_id,
                        principalTable: "sales_return",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_adjustment_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    stock_adjustment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    system_qty = table.Column<decimal>(type: "numeric", nullable: false),
                    physical_qty = table.Column<decimal>(type: "numeric", nullable: false),
                    difference_qty = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_adjustment_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_stock_adjustment_item_stock_adjustment_stock_adjustment_id",
                        column: x => x.stock_adjustment_id,
                        principalTable: "stock_adjustment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_transfer_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    stock_transfer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_transfer_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_stock_transfer_item_stock_transfer_stock_transfer_id",
                        column: x => x.stock_transfer_id,
                        principalTable: "stock_transfer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_invoice",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_no = table.Column<string>(type: "text", nullable: false),
                    invoice_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    received_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    net_amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_invoice", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_invoice_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_no = table.Column<string>(type: "text", nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expected_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_order_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_invoice",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_no = table.Column<string>(type: "text", nullable: false),
                    invoice_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    prescription_id = table.Column<Guid>(type: "uuid", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    net_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales_invoice", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_invoice_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sales_invoice_prescription_prescription_id",
                        column: x => x.prescription_id,
                        principalTable: "prescription",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prescription_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    prescription_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dosage = table.Column<string>(type: "text", nullable: true),
                    frequency = table.Column<string>(type: "text", nullable: true),
                    duration = table.Column<string>(type: "text", nullable: true),
                    quantity_prescribed = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prescription_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_prescription_item_medicine_medicine_id",
                        column: x => x.medicine_id,
                        principalTable: "medicine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prescription_item_prescription_prescription_id",
                        column: x => x.prescription_id,
                        principalTable: "prescription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_batch",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_no = table.Column<string>(type: "text", nullable: false),
                    manufacture_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    purchase_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    mrp = table.Column<decimal>(type: "numeric", nullable: false),
                    selling_price = table.Column<decimal>(type: "numeric", nullable: false),
                    gst_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    opening_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    current_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    is_blocked = table.Column<bool>(type: "boolean", nullable: false),
                    created_from_purchase_item_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_batch", x => x.id);
                    table.ForeignKey(
                        name: "fk_stock_batch_medicine_medicine_id",
                        column: x => x.medicine_id,
                        principalTable: "medicine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_invoice_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    purchase_invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_no = table.Column<string>(type: "text", nullable: false),
                    manufacture_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    purchase_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    mrp = table.Column<decimal>(type: "numeric", nullable: false),
                    selling_price = table.Column<decimal>(type: "numeric", nullable: false),
                    gst_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    received_qty = table.Column<decimal>(type: "numeric", nullable: false),
                    free_qty = table.Column<decimal>(type: "numeric", nullable: true),
                    line_total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_invoice_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_invoice_item_medicine_medicine_id",
                        column: x => x.medicine_id,
                        principalTable: "medicine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_purchase_invoice_item_purchase_invoice_purchase_invoice_id",
                        column: x => x.purchase_invoice_id,
                        principalTable: "purchase_invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    purchase_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ordered_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    free_quantity = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_order_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_order_item_medicine_medicine_id",
                        column: x => x.medicine_id,
                        principalTable: "medicine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_purchase_order_item_purchase_order_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "purchase_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    sales_invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    payment_mode = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    reference_no = table.Column<string>(type: "text", nullable: true),
                    received_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales_payment", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_payment_sales_invoice_sales_invoice_id",
                        column: x => x.sales_invoice_id,
                        principalTable: "sales_invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_invoice_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    sales_invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    mrp = table.Column<decimal>(type: "numeric", nullable: false),
                    selling_price = table.Column<decimal>(type: "numeric", nullable: false),
                    gst_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    discount_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    line_total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales_invoice_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_invoice_item_medicine_medicine_id",
                        column: x => x.medicine_id,
                        principalTable: "medicine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sales_invoice_item_sales_invoice_sales_invoice_id",
                        column: x => x.sales_invoice_id,
                        principalTable: "sales_invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sales_invoice_item_stock_batch_batch_id",
                        column: x => x.batch_id,
                        principalTable: "stock_batch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_ledger",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    medicine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_type = table.Column<int>(type: "integer", nullable: false),
                    reference_table = table.Column<string>(type: "text", nullable: true),
                    reference_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity_in = table.Column<decimal>(type: "numeric", nullable: true),
                    quantity_out = table.Column<decimal>(type: "numeric", nullable: true),
                    balance_after = table.Column<decimal>(type: "numeric", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    performed_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_ledger", x => x.id);
                    table.ForeignKey(
                        name: "fk_stock_ledger_medicine_medicine_id",
                        column: x => x.medicine_id,
                        principalTable: "medicine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_stock_ledger_stock_batch_batch_id",
                        column: x => x.batch_id,
                        principalTable: "stock_batch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_medicine_category_id",
                table: "medicine",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_medicine_manufacturer_id",
                table: "medicine",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "ix_prescription_customer_id",
                table: "prescription",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_prescription_item_medicine_id",
                table: "prescription_item",
                column: "medicine_id");

            migrationBuilder.CreateIndex(
                name: "ix_prescription_item_prescription_id",
                table: "prescription_item",
                column: "prescription_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_invoice_supplier_id",
                table: "purchase_invoice",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_invoice_item_medicine_id",
                table: "purchase_invoice_item",
                column: "medicine_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_invoice_item_purchase_invoice_id",
                table: "purchase_invoice_item",
                column: "purchase_invoice_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_order_supplier_id",
                table: "purchase_order",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_order_item_medicine_id",
                table: "purchase_order_item",
                column: "medicine_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_order_item_purchase_order_id",
                table: "purchase_order_item",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_invoice_customer_id",
                table: "sales_invoice",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_invoice_prescription_id",
                table: "sales_invoice",
                column: "prescription_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_invoice_item_batch_id",
                table: "sales_invoice_item",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_invoice_item_medicine_id",
                table: "sales_invoice_item",
                column: "medicine_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_invoice_item_sales_invoice_id",
                table: "sales_invoice_item",
                column: "sales_invoice_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_payment_sales_invoice_id",
                table: "sales_payment",
                column: "sales_invoice_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_return_item_sales_return_id",
                table: "sales_return_item",
                column: "sales_return_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_adjustment_item_stock_adjustment_id",
                table: "stock_adjustment_item",
                column: "stock_adjustment_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_batch_medicine_id",
                table: "stock_batch",
                column: "medicine_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_ledger_batch_id",
                table: "stock_ledger",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_ledger_medicine_id",
                table: "stock_ledger",
                column: "medicine_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_transfer_item_stock_transfer_id",
                table: "stock_transfer_item",
                column: "stock_transfer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alert");

            migrationBuilder.DropTable(
                name: "controlled_drug_register");

            migrationBuilder.DropTable(
                name: "prescription_item");

            migrationBuilder.DropTable(
                name: "purchase_invoice_item");

            migrationBuilder.DropTable(
                name: "purchase_order_item");

            migrationBuilder.DropTable(
                name: "sales_invoice_item");

            migrationBuilder.DropTable(
                name: "sales_payment");

            migrationBuilder.DropTable(
                name: "sales_return_item");

            migrationBuilder.DropTable(
                name: "stock_adjustment_item");

            migrationBuilder.DropTable(
                name: "stock_damage_expiry");

            migrationBuilder.DropTable(
                name: "stock_ledger");

            migrationBuilder.DropTable(
                name: "stock_transfer_item");

            migrationBuilder.DropTable(
                name: "purchase_invoice");

            migrationBuilder.DropTable(
                name: "purchase_order");

            migrationBuilder.DropTable(
                name: "sales_invoice");

            migrationBuilder.DropTable(
                name: "sales_return");

            migrationBuilder.DropTable(
                name: "stock_adjustment");

            migrationBuilder.DropTable(
                name: "stock_batch");

            migrationBuilder.DropTable(
                name: "stock_transfer");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "prescription");

            migrationBuilder.DropTable(
                name: "medicine");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "manufacturer");

            migrationBuilder.DropTable(
                name: "medicine_category");
        }
    }
}
