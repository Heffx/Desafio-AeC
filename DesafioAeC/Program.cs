using Dominio.Infraestrutura.Interfaces.Servicos;
using Dominio.Infraestrutura.Interfaces.Integracoes;
using Infraestrutura.Integracao;
using Infraestrutura.Servicos;
using Microsoft.Extensions.DependencyInjection;
using Dominio.Entidades;
using Microsoft.Data.Sqlite;

class Program
{
    static void Main(string[] args)
    {
        //Injecao de dependencia
        var serviceProvider = new ServiceCollection()
                .AddSingleton<ISeleniumServices, SeleniumServices>()
                .AddSingleton<IIntegracaoSqLiteDb, IntegracaoSqLiteDb>()
                .BuildServiceProvider();

        var sqlite = serviceProvider.GetService<IIntegracaoSqLiteDb>();
        var chrome = serviceProvider.GetService<ISeleniumServices>();
        SqliteConnection connection = null;

        try
        {
            connection = sqlite.CreateConnection();
            sqlite.CreateTable(connection);

            var cursos = ExecutaBuscaCursos(chrome);

            foreach (var curso in cursos)
            {
                sqlite.InsertData(connection, $"INSERT INTO Curso (Nome, Descricao, CargaHoraria, Professor) VALUES('{curso.Nome}', '{curso.Descricao}', '{curso.CargaHoraria}', '{curso.Professor}');");
            }

            sqlite.ReadData(connection);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            chrome.EncerrarDriver();
            sqlite.ClosedConnection(connection);
        }
    }

    public static List<Curso> ExecutaBuscaCursos(ISeleniumServices chrome)
    {
        chrome.NavegarPagina("https://www.alura.com.br/");
        chrome.PesquisaPagina("RPA");
        chrome.FiltrarPorTipo("COURSE");
        return chrome.BuscarConteudodeCursos();
    }
}