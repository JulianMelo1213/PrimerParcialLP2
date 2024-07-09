using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimerParcialLP2.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Almacen",
                columns: table => new
                {
                    AlmacenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Almacen__022A08768F36B92B", x => x.AlmacenId);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__F353C1E5C4BF2BD1", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__A430AEA3756A2187", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__61266A59AE338AB1", x => x.ProveedorId);
                });

            migrationBuilder.CreateTable(
                name: "Ajuste",
                columns: table => new
                {
                    AjusteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    AlmacenId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ajuste__AC5997E00010E8C1", x => x.AjusteId);
                    table.ForeignKey(
                        name: "FK__Ajuste__AlmacenI__32E0915F",
                        column: x => x.AlmacenId,
                        principalTable: "Almacen",
                        principalColumn: "AlmacenId");
                    table.ForeignKey(
                        name: "FK__Ajuste__Producto__31EC6D26",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "Entrada",
                columns: table => new
                {
                    EntradaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Entrada__A084667487BC8202", x => x.EntradaId);
                    table.ForeignKey(
                        name: "FK__Entrada__Product__2C3393D0",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    InventarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    AlmacenId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventar__FB8A24D7E39A7C7B", x => x.InventarioId);
                    table.ForeignKey(
                        name: "FK__Inventari__Almac__36B12243",
                        column: x => x.AlmacenId,
                        principalTable: "Almacen",
                        principalColumn: "AlmacenId");
                    table.ForeignKey(
                        name: "FK__Inventari__Produ__35BCFE0A",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "ProductoCategoria",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__EB0592BD9CE9FAF7", x => new { x.ProductoId, x.CategoriaId });
                    table.ForeignKey(
                        name: "FK__ProductoC__Categ__3E52440B",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId");
                    table.ForeignKey(
                        name: "FK__ProductoC__Produ__3D5E1FD2",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "Salida",
                columns: table => new
                {
                    SalidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Salida__DC9971633F2E8E97", x => x.SalidaId);
                    table.ForeignKey(
                        name: "FK__Salida__Producto__2F10007B",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "ProductoProveedor",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__2222C806E660A308", x => new { x.ProductoId, x.ProveedorId });
                    table.ForeignKey(
                        name: "FK__ProductoP__Produ__398D8EEE",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                    table.ForeignKey(
                        name: "FK__ProductoP__Prove__3A81B327",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "ProveedorId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ajuste_AlmacenId",
                table: "Ajuste",
                column: "AlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ajuste_ProductoId",
                table: "Ajuste",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_ProductoId",
                table: "Entrada",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_AlmacenId",
                table: "Inventario",
                column: "AlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_ProductoId",
                table: "Inventario",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCategoria_CategoriaId",
                table: "ProductoCategoria",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoProveedor_ProveedorId",
                table: "ProductoProveedor",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Salida_ProductoId",
                table: "Salida",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ajuste");

            migrationBuilder.DropTable(
                name: "Entrada");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "ProductoCategoria");

            migrationBuilder.DropTable(
                name: "ProductoProveedor");

            migrationBuilder.DropTable(
                name: "Salida");

            migrationBuilder.DropTable(
                name: "Almacen");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Producto");
        }
    }
}
