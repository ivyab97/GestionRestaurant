using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaEntrega",
                columns: table => new
                {
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntrega", x => x.FormaEntregaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderia",
                columns: table => new
                {
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderia", x => x.TipoMercaderiaId);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComandaId);
                    table.ForeignKey(
                        name: "FK_Comanda_FormaEntrega_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormaEntrega",
                        principalColumn: "FormaEntregaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderia",
                columns: table => new
                {
                    MercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Ingredientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preparacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderia", x => x.MercaderiaId);
                    table.ForeignKey(
                        name: "FK_Mercaderia_TipoMercaderia_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderia",
                        principalColumn: "TipoMercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderia",
                columns: table => new
                {
                    ComandaMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MercaderiaId = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderia", x => x.ComandaMercaderiaId);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "ComandaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Mercaderia_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderia",
                        principalColumn: "MercaderiaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FormaEntrega",
                columns: new[] { "FormaEntregaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Salon" },
                    { 2, "Delivery" },
                    { 3, "Pedidos Ya" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderia",
                columns: new[] { "TipoMercaderiaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Entrada" },
                    { 2, "Minutas" },
                    { 3, "Pastas" },
                    { 4, "Parrilla" },
                    { 5, "Pizzas" },
                    { 6, "Sandwich" },
                    { 7, "Ensaladas" },
                    { 8, "Bebidas" },
                    { 9, "Cerveza Artesanal" },
                    { 10, "Postres" }
                });

            migrationBuilder.InsertData(
                table: "Mercaderia",
                columns: new[] { "MercaderiaId", "Imagen", "Ingredientes", "Nombre", "Precio", "Preparacion", "TipoMercaderiaId" },
                values: new object[,]
                {
                    { 1, "https://img.freepik.com/foto-gratis/antipasto-varios-aperitivos-tabla-cortar-jamon-serrano-salami-coppa-queso-aceitunas-pared-gris-vista-superior_89816-11473.jpg?size=626&ext=jpg", "jamón crudo, salame, queso de cabra, aceitunas", "Tabla de fiambres", 1500, "Colocar los fiambres y el queso de cabra en una tabla de madera junto con las aceitunas. Servir frío.", 1 },
                    { 2, "https://th.bing.com/th/id/OIP.vIA6RVih2inqxusXcrXYWgHaE7?pid=ImgDet&rs=1", "pechuga de pollo, pimiento rojo, pimiento verde, cebolla, aceite de oliva, sal, pimienta", "Pinchos de pollo", 600, "Cortar la pechuga de pollo en cubos y las verduras en trozos similares. En un recipiente, mezclar el aceite de oliva con la sal y la pimienta. Ensartar los cubos de pollo y los trozos de verduras en palitos de brochette y pintarlos con la mezcla de ace...", 1 },
                    { 3, "https://i.pinimg.com/originals/02/fe/88/02fe8898eef54865652e7f64837c880e.jpg", "milanesa de carne, salsa de tomate, jamón cocido, queso mozzarella", "Milanesa napolitana", 900, "Freír la milanesa de carne y reservar. En una cacerola, calentar la salsa de tomate. Colocar la milanesa en una fuente para horno y cubrir con la salsa de tomate, jamón cocido y queso mozzarella. Llevar al horno precalentado a 180°C durante 10-15 minut...", 2 },
                    { 4, "https://media-cdn.tripadvisor.com/media/photo-s/06/e3/d3/e6/ushuaia-frankfurt.jpg", "lomo de ternera, lechuga, tomate, mayonesa, pan francés", "Lomo completo", 900, "Cocinar el lomo a la parrilla o en una sartén. Cortar el pan en dos y tostarlo. Untar la mayonesa en el pan, colocar la lechuga y el tomate encima del pan. Colocar el lomo en el pan y cerrar.", 2 },
                    { 5, "https://i2.wp.com/www.cocinaeficaz.com/wp-content/uploads/2016/09/%C3%B1oquis-de-papas-caseros-con-salsa-de-tomate.jpg?fit=800%2C598&ssl=1", "ñoquis, tomates perita, cebolla, ajo, aceite de oliva, sal, pimienta, queso parmesano", "Ñoquis con salsa de tomate casera", 1000, "Cocinar los ñoquis en agua hirviendo con sal hasta que estén al dente. En una sartén, dorar la cebolla y el ajo en aceite de oliva. Agregar los tomates perita picados y cocinar por unos minutos. Agregar sal y pimienta al gusto. Mezclar la salsa de toma...", 3 },
                    { 6, "https://www.ricettaidea.it/articoli/ricette-regionali/lazio/original_spaghetti-alla-carbonara.jpg", "spaghetti, panceta, huevo, queso parmesano, aceite de oliva, sal, pimienta", "Spaghetti a la carbonara", 1000, "Cocinar los spaghetti en agua hirviendo con sal hasta que estén al dente. En una sartén, dorar la panceta en aceite de oliva. En un tazón, batir el huevo y agregar el queso parmesano rallado. Agregar la panceta y el aceite de oliva a la mezcla de huevo...", 3 },
                    { 7, "https://th.bing.com/th/id/R.b02985c27cbf308e10a7b921b0c66a11?rik=ERI1ak13hDsmPg&pid=ImgRaw&r=0", "bife de chorizo, sal gruesa, pimienta", "Bife de chorizo", 1500, "Precalentar la parrilla a fuego medio-alto. Salar y pimentar el bife de chorizo al gusto. Colocar el bife en la parrilla y cocinar por 4-5 minutos de cada lado para un término medio. Dejar reposar el bife por unos minutos antes de cortarlo y servirlo c...", 4 },
                    { 8, "https://th.bing.com/th/id/OIP.Os17j7OwByy5KCaluxoSowHaEo?pid=ImgDet&rs=1", "carne (lomo o entraña), cebolla, morrón, tomates cherry, aceite de oliva, sal, pimienta", "Brochette de carne y verduras", 950, "Cortar la carne en cubos y las verduras en trozos del mismo tamaño. En un recipiente, mezclar el aceite de oliva con sal y pimienta al gusto. Insertar los cubos de carne y las verduras en palillos de brochette alternando entre carne y verduras. Precale...", 4 },
                    { 9, "https://th.bing.com/th/id/OIP.Xlx7oZVd_khsvg4O92O6DgHaE8?pid=ImgDet&rs=1", "salsa de tomate, mozzarella, albahaca fresca", "Pizza Margherita", 1500, "Precalentar el horno a 250°C. Extender la masa de pizza y cubrirla con la salsa de tomate. Agregar la mozzarella y espolvorear albahaca fresca al gusto. Hornear durante 10-12 minutos o hasta que la pizza esté dorada y crujiente. Servir caliente.", 5 },
                    { 10, "https://th.bing.com/th/id/OIP.bj9ikcQ_3kP7VbipYtFnfgHaFC?pid=ImgDet&rs=1", "salsa de tomate, mozzarella, jamón, champiñones", "Pizza de jamón y hongos", 1600, "Precalentar el horno a 250°C. Extender la masa de pizza y cubrirla con la salsa de tomate. Agregar la mozzarella y luego el jamón y los champiñones cortados en rodajas. Hornear durante 10-12 minutos o hasta que la pizza esté dorada y crujiente. Servir ...", 5 },
                    { 11, "https://th.bing.com/th/id/OIP.ntyZXTN9XOti6HYA3RTj6AHaFj?pid=ImgDet&rs=1", "pan de molde, pollo asado, queso cheddar, lechuga, tomate, mayonesa", "Sandwich de pollo y queso", 900, "Tostar el pan de molde en una sartén o tostadora. En una sartén aparte, calentar el pollo asado y añadir el queso cheddar hasta que se derrita. Colocar la lechuga y el tomate en una rebanada de pan, luego añadir el pollo y el queso derretido. Cubrir co...", 6 },
                    { 12, "https://enteratedelicias.com/wp-content/uploads/2022/03/cuantas-calorias-tiene-un-sandwich-de-jamon-y-queso.jpg", "pan francés, jamón cocido, queso provolone, tomate, lechuga, mayonesa", "Sandwich de jamón y queso", 650, "Cortar el pan francés en dos y tostarlo ligeramente en una sartén. Añadir el jamón y el queso provolone, y ponerlo a gratinar en el horno durante unos minutos. Agregar el tomate y la lechuga al gusto, y cubrir con la mayonesa antes de cerrar el sandwich.", 6 },
                    { 13, "https://okdiario.com/img/recetas/2015/08/30/ensalada-cesar.jpg", "lechuga, pollo asado, croutones, queso parmesano, aderezo César", "Ensalada César", 650, "Lavar y picar la lechuga, y colocarla en un bol. Cortar el pollo asado en trozos pequeños y agregarlos a la ensalada. Añadir los croutones y el queso parmesano. Cubrir con el aderezo César y mezclar bien.", 7 },
                    { 14, "https://th.bing.com/th/id/OIP.-Kjln5nFsQ53ho-3sXaMJAHaE8?pid=ImgDet&rs=1", "lechuga, salmón ahumado, huevo duro, cebolla morada, tomate cherry, aceitunas, vinagreta de limón", "Ensalada de salmón", 750, "Lavar y picar la lechuga, y colocarla en un bol. Agregar el salmón ahumado cortado en trozos pequeños, los huevos duros picados, la cebolla morada en rodajas finas, los tomates cherry cortados por la mitad y las aceitunas. Cubrir con la vinagreta de li...", 7 },
                    { 15, "https://th.bing.com/th/id/OIP.EOZRV17gk8fwRwLpm8SVTQHaHa?pid=ImgDet&rs=1", "limones, azúcar, agua, hielo", "Limonada natural", 350, "Cortar los limones en mitades y exprimir el jugo en una jarra. Agregar azúcar al gusto y mezclar bien. Añadir agua fría y hielo, y revolver hasta que se disuelva el azúcar. Servir en vasos con hielo.", 8 },
                    { 16, "https://th.bing.com/th/id/OIP.-ZnJCFtxcJvHNcRitA2_eAHaHa?pid=ImgDet&rs=1", "agua con gas, frutos rojos (frambuesas, moras, arándanos), menta fresca", "Agua saborizada de frutos rojos", 400, "En una jarra, colocar los frutos rojos y la menta fresca. Agregar agua con gas y mezclar bien. Dejar reposar en la nevera por al menos una hora antes de servir para que los sabores se integren. Servir en vasos con hielo.", 8 },
                    { 17, "https://s3-us-west-2.amazonaws.com/craftbeerdotcom/wp-content/uploads/2016/02/01141533/New-IPA-feature.jpg", "agua, maltas (pale ale, caramelo), lúpulos (Citra, Centennial), levadura", "India Pale Ale", 900, "En una olla, calentar agua hasta alcanzar la temperatura adecuada. Agregar las maltas y dejar reposar durante una hora. Luego, agregar los lúpulos y dejar hervir durante una hora más. Por último, agregar la levadura y dejar fermentar durante una semana...", 9 },
                    { 18, "https://images-na.ssl-images-amazon.com/images/I/41Er65V%2BEeL.jpg", "agua, maltas (pale ale, chocolate, caramelo), lúpulos (Nugget), levadura", "Stout", 900, "En una olla, calentar agua hasta alcanzar la temperatura adecuada. Agregar las maltas y dejar reposar durante una hora. Luego, agregar los lúpulos y dejar hervir durante una hora más. Por último, agregar la levadura y dejar fermentar durante una semana...", 9 },
                    { 19, "https://th.bing.com/th/id/OIP.xRsfgcAkr1VfqXJ2WFVXXAHaEK?pid=ImgDet&rs=1", "galletas de vainilla, queso crema, azúcar, huevos, crema de leche, frutos rojos", "Cheesecake de frutos rojos", 800, "Triturar las galletas y mezclarlas con mantequilla derretida. Cubrir un molde con esta mezcla y reservar en la nevera. Batir el queso crema con el azúcar y añadir los huevos, uno a uno, sin dejar de batir. Incorporar la crema de leche y mezclar bien. V...", 10 },
                    { 20, "https://www.yummytummyaarthi.com/wp-content/uploads/2015/09/4-2.jpg", "harina, cacao en polvo, azúcar, huevos, aceite de girasol, avellanas, crema de leche", "Torta de chocolate y avellanas", 900, "En un bol, mezclar la harina con el cacao y el azúcar. Añadir los huevos y el aceite de girasol y mezclar hasta obtener una masa homogénea. Agregar las avellanas picadas y mezclar bien. Verter la masa en un molde y hornear durante unos 35 minutos a 180...", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_FormaEntregaId",
                table: "Comanda",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_ComandaId",
                table: "ComandaMercaderia",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_MercaderiaId",
                table: "ComandaMercaderia",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderia_Nombre",
                table: "Mercaderia",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderia_TipoMercaderiaId",
                table: "Mercaderia",
                column: "TipoMercaderiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderia");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Mercaderia");

            migrationBuilder.DropTable(
                name: "FormaEntrega");

            migrationBuilder.DropTable(
                name: "TipoMercaderia");
        }
    }
}
