using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public DbSet<TipoMercaderia> TipoMercaderia { get; set; }
        public DbSet<Mercaderia> Mercaderia { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderia { get; set; }
        public DbSet<Comanda> Comanda { get; set; }
        public DbSet<FormaEntrega> FormaEntrega { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TipoMercaderia>(entity =>
            {
                entity.ToTable("TipoMercaderia");
                entity.HasKey(tmi => tmi.TipoMercaderiaId);
                entity.Property(tmi => tmi.TipoMercaderiaId).ValueGeneratedOnAdd();
                entity.Property(tmi => tmi.TipoMercaderiaId)
                      .IsRequired();
                entity.Property(d => d.Descripcion)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany<Mercaderia>(m => m.Mercaderia)                   
                      .WithOne(tp => tp.TipoMercaderia)
                      .HasForeignKey(tpi => tpi.TipoMercaderiaId);

                entity.HasData(
                        new TipoMercaderia
                        {
                            TipoMercaderiaId=1,
                            Descripcion= "Entrada"
                        },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 2,
                            Descripcion = "Minutas"
                        },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 3,
                            Descripcion = "Pastas"
                        },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 4,
                            Descripcion = "Parrilla"
                        },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 5,
                            Descripcion = "Pizzas"
                        },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 6,
                            Descripcion = "Sandwich"
                        },
                         new TipoMercaderia
                         {
                             TipoMercaderiaId = 7,
                             Descripcion = "Ensaladas"
                         },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 8,
                            Descripcion = "Bebidas"
                        },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 9,
                            Descripcion = "Cerveza Artesanal"

                        },
                        new TipoMercaderia
                        {
                            TipoMercaderiaId = 10,
                            Descripcion = "Postres"
                        }
                       );
            });

            modelBuilder.Entity<Mercaderia>(entity =>
            {
                entity.ToTable("Mercaderia");
                entity.HasKey(mi => mi.MercaderiaId);
                entity.Property(mi => mi.MercaderiaId).ValueGeneratedOnAdd();
                entity.Property(mi => mi.MercaderiaId)
                      .IsRequired();
                entity.Property(n => n.Nombre)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.HasIndex(n => n.Nombre).IsUnique();
                entity.Property(tp => tp.TipoMercaderiaId)
                      .IsRequired();
                entity.Property(p => p.Precio)
                      .IsRequired();
                entity.Property(p => p.Precio)
                      .IsRequired();
                entity.Property(i => i.Ingredientes)
                      .IsRequired()
                      .HasMaxLength(255);
                entity.Property(p => p.Preparacion)
                      .IsRequired()
                      .HasMaxLength(255);
                entity.Property(i => i.Imagen)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.HasOne<TipoMercaderia>(tm => tm.TipoMercaderia)
                      .WithMany(m => m.Mercaderia)
                      .HasForeignKey(tmi => tmi.TipoMercaderiaId);

                entity.HasMany<ComandaMercaderia>(cm => cm.ComandaMercaderia)
                      .WithOne(m => m.Mercaderia)
                      .HasForeignKey(mi => mi.MercaderiaId)
                      .OnDelete(DeleteBehavior.Restrict);


                entity.HasData(
                        new Mercaderia
                        {
                            MercaderiaId=1,
                            Nombre= "Tabla de fiambres",
                            TipoMercaderiaId = 1,
                            Precio = 1500,
                            Ingredientes= "jamón crudo, salame, queso de cabra, aceitunas",
                            Preparacion= "Colocar los fiambres y el queso de cabra en una tabla de madera junto con las aceitunas. Servir frío.",
                            Imagen= "https://img.freepik.com/foto-gratis/antipasto-varios-aperitivos-tabla-cortar-jamon-serrano-salami-coppa-queso-aceitunas-pared-gris-vista-superior_89816-11473.jpg?size=626&ext=jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 2,
                            Nombre = "Pinchos de pollo",
                            TipoMercaderiaId=1,
                            Precio =600,
                            Ingredientes = "pechuga de pollo, pimiento rojo, pimiento verde, cebolla, aceite de oliva, sal, pimienta",
                            Preparacion = "Cortar la pechuga de pollo en cubos y las verduras en trozos similares. En un recipiente, mezclar el aceite de oliva con la sal y la pimienta. Ensartar los cubos de pollo y los trozos de verduras en palitos de brochette y pintarlos con la mezcla de ace...",
                            Imagen = "https://th.bing.com/th/id/OIP.vIA6RVih2inqxusXcrXYWgHaE7?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 3,
                            Nombre = "Milanesa napolitana",
                            TipoMercaderiaId =2,
                            Precio =900,
                            Ingredientes = "milanesa de carne, salsa de tomate, jamón cocido, queso mozzarella",
                            Preparacion = "Freír la milanesa de carne y reservar. En una cacerola, calentar la salsa de tomate. Colocar la milanesa en una fuente para horno y cubrir con la salsa de tomate, jamón cocido y queso mozzarella. Llevar al horno precalentado a 180°C durante 10-15 minut...",
                            Imagen = "https://i.pinimg.com/originals/02/fe/88/02fe8898eef54865652e7f64837c880e.jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 4,
                            Nombre = "Lomo completo",
                            TipoMercaderiaId =2,
                            Precio =900,
                            Ingredientes = "lomo de ternera, lechuga, tomate, mayonesa, pan francés",
                            Preparacion = "Cocinar el lomo a la parrilla o en una sartén. Cortar el pan en dos y tostarlo. Untar la mayonesa en el pan, colocar la lechuga y el tomate encima del pan. Colocar el lomo en el pan y cerrar.",
                            Imagen = "https://media-cdn.tripadvisor.com/media/photo-s/06/e3/d3/e6/ushuaia-frankfurt.jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 5,
                            Nombre = "Ñoquis con salsa de tomate casera",
                            TipoMercaderiaId =3,
                            Precio =1000,
                            Ingredientes = "ñoquis, tomates perita, cebolla, ajo, aceite de oliva, sal, pimienta, queso parmesano",
                            Preparacion = "Cocinar los ñoquis en agua hirviendo con sal hasta que estén al dente. En una sartén, dorar la cebolla y el ajo en aceite de oliva. Agregar los tomates perita picados y cocinar por unos minutos. Agregar sal y pimienta al gusto. Mezclar la salsa de toma...",
                            Imagen = "https://i2.wp.com/www.cocinaeficaz.com/wp-content/uploads/2016/09/%C3%B1oquis-de-papas-caseros-con-salsa-de-tomate.jpg?fit=800%2C598&ssl=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 6,
                            Nombre = "Spaghetti a la carbonara",
                            TipoMercaderiaId =3,
                            Precio =1000,
                            Ingredientes = "spaghetti, panceta, huevo, queso parmesano, aceite de oliva, sal, pimienta",
                            Preparacion = "Cocinar los spaghetti en agua hirviendo con sal hasta que estén al dente. En una sartén, dorar la panceta en aceite de oliva. En un tazón, batir el huevo y agregar el queso parmesano rallado. Agregar la panceta y el aceite de oliva a la mezcla de huevo...",
                            Imagen = "https://www.ricettaidea.it/articoli/ricette-regionali/lazio/original_spaghetti-alla-carbonara.jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 7,
                            Nombre = "Bife de chorizo",
                            TipoMercaderiaId =4,
                            Precio =1500,
                            Ingredientes = "bife de chorizo, sal gruesa, pimienta",
                            Preparacion = "Precalentar la parrilla a fuego medio-alto. Salar y pimentar el bife de chorizo al gusto. Colocar el bife en la parrilla y cocinar por 4-5 minutos de cada lado para un término medio. Dejar reposar el bife por unos minutos antes de cortarlo y servirlo c...",
                            Imagen = "https://th.bing.com/th/id/R.b02985c27cbf308e10a7b921b0c66a11?rik=ERI1ak13hDsmPg&pid=ImgRaw&r=0"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 8,
                            Nombre = "Brochette de carne y verduras",
                            TipoMercaderiaId =4,
                            Precio =950,
                            Ingredientes = "carne (lomo o entraña), cebolla, morrón, tomates cherry, aceite de oliva, sal, pimienta",
                            Preparacion = "Cortar la carne en cubos y las verduras en trozos del mismo tamaño. En un recipiente, mezclar el aceite de oliva con sal y pimienta al gusto. Insertar los cubos de carne y las verduras en palillos de brochette alternando entre carne y verduras. Precale...",
                            Imagen = "https://th.bing.com/th/id/OIP.Os17j7OwByy5KCaluxoSowHaEo?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 9,
                            Nombre = "Pizza Margherita",
                            TipoMercaderiaId =5,
                            Precio =1500,
                            Ingredientes = "salsa de tomate, mozzarella, albahaca fresca",
                            Preparacion = "Precalentar el horno a 250°C. Extender la masa de pizza y cubrirla con la salsa de tomate. Agregar la mozzarella y espolvorear albahaca fresca al gusto. Hornear durante 10-12 minutos o hasta que la pizza esté dorada y crujiente. Servir caliente.",
                            Imagen = "https://th.bing.com/th/id/OIP.Xlx7oZVd_khsvg4O92O6DgHaE8?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 10,
                            Nombre = "Pizza de jamón y hongos",
                            TipoMercaderiaId =5,
                            Precio =1600,
                            Ingredientes = "salsa de tomate, mozzarella, jamón, champiñones",
                            Preparacion = "Precalentar el horno a 250°C. Extender la masa de pizza y cubrirla con la salsa de tomate. Agregar la mozzarella y luego el jamón y los champiñones cortados en rodajas. Hornear durante 10-12 minutos o hasta que la pizza esté dorada y crujiente. Servir ...",
                            Imagen = "https://th.bing.com/th/id/OIP.bj9ikcQ_3kP7VbipYtFnfgHaFC?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 11,
                            Nombre = "Sandwich de pollo y queso",
                            TipoMercaderiaId =6,
                            Precio =900,
                            Ingredientes = "pan de molde, pollo asado, queso cheddar, lechuga, tomate, mayonesa",
                            Preparacion = "Tostar el pan de molde en una sartén o tostadora. En una sartén aparte, calentar el pollo asado y añadir el queso cheddar hasta que se derrita. Colocar la lechuga y el tomate en una rebanada de pan, luego añadir el pollo y el queso derretido. Cubrir co...",
                            Imagen = "https://th.bing.com/th/id/OIP.ntyZXTN9XOti6HYA3RTj6AHaFj?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 12,
                            Nombre = "Sandwich de jamón y queso",
                            TipoMercaderiaId =6,
                            Precio =650,
                            Ingredientes = "pan francés, jamón cocido, queso provolone, tomate, lechuga, mayonesa",
                            Preparacion = "Cortar el pan francés en dos y tostarlo ligeramente en una sartén. Añadir el jamón y el queso provolone, y ponerlo a gratinar en el horno durante unos minutos. Agregar el tomate y la lechuga al gusto, y cubrir con la mayonesa antes de cerrar el sandwich.",
                            Imagen = "https://enteratedelicias.com/wp-content/uploads/2022/03/cuantas-calorias-tiene-un-sandwich-de-jamon-y-queso.jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 13,
                            Nombre = "Ensalada César",
                            TipoMercaderiaId =7,
                            Precio =650,
                            Ingredientes = "lechuga, pollo asado, croutones, queso parmesano, aderezo César",
                            Preparacion = "Lavar y picar la lechuga, y colocarla en un bol. Cortar el pollo asado en trozos pequeños y agregarlos a la ensalada. Añadir los croutones y el queso parmesano. Cubrir con el aderezo César y mezclar bien.",
                            Imagen = "https://okdiario.com/img/recetas/2015/08/30/ensalada-cesar.jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 14,
                            Nombre = "Ensalada de salmón",
                            TipoMercaderiaId =7,
                            Precio =750,
                            Ingredientes = "lechuga, salmón ahumado, huevo duro, cebolla morada, tomate cherry, aceitunas, vinagreta de limón",
                            Preparacion = "Lavar y picar la lechuga, y colocarla en un bol. Agregar el salmón ahumado cortado en trozos pequeños, los huevos duros picados, la cebolla morada en rodajas finas, los tomates cherry cortados por la mitad y las aceitunas. Cubrir con la vinagreta de li...",
                            Imagen = "https://th.bing.com/th/id/OIP.-Kjln5nFsQ53ho-3sXaMJAHaE8?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 15,
                            Nombre = "Limonada natural",
                            TipoMercaderiaId = 8,
                            Precio =350,
                            Ingredientes = "limones, azúcar, agua, hielo",
                            Preparacion = "Cortar los limones en mitades y exprimir el jugo en una jarra. Agregar azúcar al gusto y mezclar bien. Añadir agua fría y hielo, y revolver hasta que se disuelva el azúcar. Servir en vasos con hielo.",
                            Imagen = "https://th.bing.com/th/id/OIP.EOZRV17gk8fwRwLpm8SVTQHaHa?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 16,
                            Nombre = "Agua saborizada de frutos rojos",
                            TipoMercaderiaId =8,
                            Precio = 400,
                            Ingredientes = "agua con gas, frutos rojos (frambuesas, moras, arándanos), menta fresca",
                            Preparacion = "En una jarra, colocar los frutos rojos y la menta fresca. Agregar agua con gas y mezclar bien. Dejar reposar en la nevera por al menos una hora antes de servir para que los sabores se integren. Servir en vasos con hielo.",
                            Imagen = "https://th.bing.com/th/id/OIP.-ZnJCFtxcJvHNcRitA2_eAHaHa?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 17,
                            Nombre = "India Pale Ale",
                            TipoMercaderiaId =9,
                            Precio =900,
                            Ingredientes = "agua, maltas (pale ale, caramelo), lúpulos (Citra, Centennial), levadura",
                            Preparacion = "En una olla, calentar agua hasta alcanzar la temperatura adecuada. Agregar las maltas y dejar reposar durante una hora. Luego, agregar los lúpulos y dejar hervir durante una hora más. Por último, agregar la levadura y dejar fermentar durante una semana...",
                            Imagen = "https://s3-us-west-2.amazonaws.com/craftbeerdotcom/wp-content/uploads/2016/02/01141533/New-IPA-feature.jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 18,
                            Nombre = "Stout",
                            TipoMercaderiaId =9,
                            Precio =900,
                            Ingredientes = "agua, maltas (pale ale, chocolate, caramelo), lúpulos (Nugget), levadura",
                            Preparacion = "En una olla, calentar agua hasta alcanzar la temperatura adecuada. Agregar las maltas y dejar reposar durante una hora. Luego, agregar los lúpulos y dejar hervir durante una hora más. Por último, agregar la levadura y dejar fermentar durante una semana...",
                            Imagen = "https://images-na.ssl-images-amazon.com/images/I/41Er65V%2BEeL.jpg"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 19,
                            Nombre = "Cheesecake de frutos rojos",
                            TipoMercaderiaId =10,
                            Precio =800,
                            Ingredientes = "galletas de vainilla, queso crema, azúcar, huevos, crema de leche, frutos rojos",
                            Preparacion = "Triturar las galletas y mezclarlas con mantequilla derretida. Cubrir un molde con esta mezcla y reservar en la nevera. Batir el queso crema con el azúcar y añadir los huevos, uno a uno, sin dejar de batir. Incorporar la crema de leche y mezclar bien. V...",
                            Imagen = "https://th.bing.com/th/id/OIP.xRsfgcAkr1VfqXJ2WFVXXAHaEK?pid=ImgDet&rs=1"
                        },
                        new Mercaderia
                        {
                            MercaderiaId = 20,
                            Nombre = "Torta de chocolate y avellanas",
                            TipoMercaderiaId =10,
                            Precio =900,
                            Ingredientes = "harina, cacao en polvo, azúcar, huevos, aceite de girasol, avellanas, crema de leche",
                            Preparacion = "En un bol, mezclar la harina con el cacao y el azúcar. Añadir los huevos y el aceite de girasol y mezclar hasta obtener una masa homogénea. Agregar las avellanas picadas y mezclar bien. Verter la masa en un molde y hornear durante unos 35 minutos a 180...",
                            Imagen = "https://www.yummytummyaarthi.com/wp-content/uploads/2015/09/4-2.jpg"
                        }
                    );
            });

            modelBuilder.Entity<ComandaMercaderia>(entity =>
            {
                entity.ToTable("ComandaMercaderia");
                entity.HasKey(cm => cm.ComandaMercaderiaId);
                entity.Property(cm => cm.ComandaMercaderiaId).ValueGeneratedOnAdd();
                entity.Property(cm => cm.ComandaMercaderiaId)
                      .IsRequired();
                entity.Property(ci => ci.ComandaId)
                      .IsRequired();

                entity.HasOne<Mercaderia>(m => m.Mercaderia)
                       .WithMany(cm => cm.ComandaMercaderia)
                       .HasForeignKey(mi => mi.MercaderiaId)
                       .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne<Comanda>(c => c.Comanda)
                     .WithMany(cm => cm.ComandaMercaderia)
                     .HasForeignKey(ci => ci.ComandaId)
                     .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Comanda>(entity =>
            {
                entity.ToTable("Comanda");
                entity.HasKey(ci => ci.ComandaId);
                entity.Property(ci => ci.ComandaId)
                      .IsRequired();
                entity.Property(fei => fei.FormaEntregaId)
                      .IsRequired();
                entity.Property(pt => pt.PrecioTotal)
                      .IsRequired();
                entity.Property(f => f.Fecha)
                      .IsRequired();
                
                entity.HasOne<FormaEntrega>(fe => fe.FormaEntrega)
                      .WithMany(c => c.Comanda)
                      .HasForeignKey(fei => fei.FormaEntregaId);

                entity.HasMany<ComandaMercaderia>(cm => cm.ComandaMercaderia)
                      .WithOne(c => c.Comanda)
                      .HasForeignKey(ci => ci.ComandaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FormaEntrega>(entity =>
            {
                entity.ToTable("FormaEntrega");
                entity.HasKey(fei => fei.FormaEntregaId);
                entity.Property(fei => fei.FormaEntregaId)
                      .IsRequired();
                entity.Property(d => d.Descripcion)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany<Comanda>(c => c.Comanda)
                      .WithOne(fe => fe.FormaEntrega)
                      .HasForeignKey(fei => fei.FormaEntregaId);

                entity.HasData(
                        new FormaEntrega
                        {
                            FormaEntregaId = 1,
                            Descripcion = "Salon"
                        },
                        new FormaEntrega
                        {
                            FormaEntregaId = 2,
                            Descripcion = "Delivery"
                        },
                        new FormaEntrega
                        {
                            FormaEntregaId = 3,
                            Descripcion = "Pedidos Ya"
                        }
                    );
            });

        }
    }
}
