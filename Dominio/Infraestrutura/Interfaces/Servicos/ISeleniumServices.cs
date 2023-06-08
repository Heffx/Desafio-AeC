using OpenQA.Selenium;

namespace Dominio.Infraestrutura.Interfaces.Servicos
{
    public interface ISeleniumServices
    {
        public IWebDriver NavegarPagina(string url);
        public IWebDriver PesquisaPagina(IWebDriver driver, string itemPesquisa);
    }
}
