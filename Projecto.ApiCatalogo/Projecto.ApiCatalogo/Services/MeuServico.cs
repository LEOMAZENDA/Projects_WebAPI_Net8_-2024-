namespace Projecto.ApiCatalogo.Services;

public class MeuServico : IMeuServico
{
    public string Saudacao(string nome)
    {
        return $"Bem-Vindo ao mundo, {nome}, \n\n {DateTime.UtcNow}";
    }
}