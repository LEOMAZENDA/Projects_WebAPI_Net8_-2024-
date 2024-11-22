using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projecto.ApiCatalogo.Migrations
{
    public partial class PopulaProductos : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            // Inserindo produtos para a categoria "Bebidas" (CategoriaId = 1)
            mb.Sql("INSERT INTO Productos (Nome, Descricao, ImagemUrl, Preco, Estoque, DataCadastro, CategotiaId) VALUES ('Coca-Cola Diet', 'Refrigerante de cola', 'coca_cola.jpg', 5.99, 100, NOW(), 1)");
            mb.Sql("INSERT INTO Productos (Nome, Descricao, ImagemUrl, Preco, Estoque, DataCadastro, CategotiaId) VALUES ('Suco de Laranja', 'Suco natural de laranja', 'suco_laranja.jpg', 4.50, 50, NOW(), 1)");

            // Inserindo produtos para a categoria "Lanches" (CategoriaId = 2)
            mb.Sql("INSERT INTO Productos (Nome, Descricao, ImagemUrl, Preco, Estoque, DataCadastro, CategotiaId) VALUES ('Hambúrguer', 'Hambúrguer artesanal com queijo', 'hamburguer.jpg', 15.00, 200, NOW(), 2)");
            mb.Sql("INSERT INTO Productos (Nome, Descricao, ImagemUrl, Preco, Estoque, DataCadastro, CategotiaId) VALUES ('Sanduíche de Frango', 'Sanduíche de frango com molho especial', 'sanduiche_frango.jpg', 12.50, 150, NOW(), 2)");

            // Inserindo produtos para a categoria "Sobremesas" (CategoriaId = 3)
            mb.Sql("INSERT INTO Productos (Nome, Descricao, ImagemUrl, Preco, Estoque, DataCadastro, CategotiaId) VALUES ('Bolo de Chocolate', 'Bolo de chocolate com cobertura cremosa', 'bolo_chocolate.jpg', 10.00, 80, NOW(), 3)");
            mb.Sql("INSERT INTO Productos (Nome, Descricao, ImagemUrl, Preco, Estoque, DataCadastro, CategotiaId) VALUES ('Pudim', 'Pudim de leite condensado', 'pudim.jpg', 6.50, 90, NOW(), 3)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            // Remover os produtos inseridos
            mb.Sql("DELETE FROM Productos");
        }
    }
}
