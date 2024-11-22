using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projecto.ApiCatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            // Inserir dados diretamente usando SQL, utilizando NOW() para obter a data atual no MySQL
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl, DataCadastro) VALUES ('Bebidas', 'bebidas.jpg', NOW())");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl, DataCadastro) VALUES ('Lanches', 'lanches.jpg', NOW())");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl, DataCadastro) VALUES ('Sobremesas', 'sobremesas.jpg', NOW())");
        }

        protected override void Down(MigrationBuilder mb)
        {
            // Remover os dados inseridos na operação Down
            mb.Sql("DELETE FROM Categorias WHERE Nome IN ('Bebidas', 'Lanches', 'Sobremesas')");
        }
    }
}