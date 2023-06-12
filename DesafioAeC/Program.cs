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

        var chrome = serviceProvider.GetService<ISeleniumServices>();
        ExecutaBuscaCursos(chrome);
    }

    public static void ExecutaBuscaCursos(ISeleniumServices chrome)
    {
        chrome.NavegarPagina("https://www.alura.com.br/");
        chrome.PesquisaPagina("RPA");
        chrome.FiltrarPorTipo("COURSE");
        chrome.BuscarConteudodeCursos();
    }
}