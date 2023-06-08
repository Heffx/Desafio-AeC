using Dominio.Infraestrutura.Interfaces.Servicos;
using Dominio.Servicos;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        //Injecao de dependencia
        var serviceProvider = new ServiceCollection()
                .AddSingleton<ISeleniumServices, SeleniumServices>()
                .BuildServiceProvider();
    }
}