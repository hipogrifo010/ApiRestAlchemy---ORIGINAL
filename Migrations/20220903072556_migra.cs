using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRestAlchemy.Migrations
{
    public partial class migra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "PeliculaOserie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaDeCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    PersonajesAsociados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaOserie", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_PeliculaOserie_Genero_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genero",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personaje",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Historia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personaje", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Personaje_PeliculaOserie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "PeliculaOserie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genero",
                columns: new[] { "GenreId", "Image", "Nombre" },
                values: new object[] { 1, " ", "Aventura/Fantasia" });

            migrationBuilder.InsertData(
                table: "Genero",
                columns: new[] { "GenreId", "Image", "Nombre" },
                values: new object[] { 2, " ", "Infantil/Fantasia" });

            migrationBuilder.InsertData(
                table: "PeliculaOserie",
                columns: new[] { "MovieId", "Calificacion", "FechaDeCreacion", "GenreId", "Imagen", "PersonajesAsociados", "Titulo" },
                values: new object[] { 1, 4, "22/04/2001", 1, " ", "Shrek,Burro,Fiora", "Shrek" });

            migrationBuilder.InsertData(
                table: "PeliculaOserie",
                columns: new[] { "MovieId", "Calificacion", "FechaDeCreacion", "GenreId", "Imagen", "PersonajesAsociados", "Titulo" },
                values: new object[] { 2, 4, "22/11/1995", 2, " ", "Woody,Buzz Lightyear,Andy", "Toy Story" });

            migrationBuilder.InsertData(
                table: "Personaje",
                columns: new[] { "CharacterId", "Edad", "Historia", "Imagen", "MovieId", "Nombre", "Peso" },
                values: new object[,]
                {
                    { 1, 30, "Un ogro llamado Shrek vive en su pantano, pero su preciada soledad se ve súbitamente interrumpida por la invasión de los ruidosos personajes de los cuentos de hadas", " ", 1, "Shrek", "230kg" },
                    { 2, 16, " es un asno parlante que se convierte en el compañero de aventuras de Shrek y se hace su mejor amigo, aunque al principio el ogro no le soportaba y lo repudia, cuando Lord Farquaad decide eliminar a las criaturas mágicas del reino y su dueña decide entregarlo.", " ", 1, "Burro", "200kg" },
                    { 3, 17, "Woody es un vaquero de juguete con una cuerda en la parte de atrás, que al tirar de ella dice frases tales como: Hay una serpiente en mi bota o Alguien ha envenenado el abrevadero. Woody es el juguete preferido de Andy,hasta que llega Buzz Lightyear", " ", 2, "Woody", "160g" },
                    { 4, 16, " es un juguete con forma de guerrero espacial, el cual llega hasta las manos de Andy, un niño con una gran colección de juguetes. En casa de Andy conocerá al resto de juguetes como son Woody, el Sr. Patata o Rex, entre otros.", " ", 2, "Buzz Lightyear", "1kg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaOserie_GenreId",
                table: "PeliculaOserie",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Personaje_MovieId",
                table: "Personaje",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personaje");

            migrationBuilder.DropTable(
                name: "PeliculaOserie");

            migrationBuilder.DropTable(
                name: "Genero");
        }
    }
}
