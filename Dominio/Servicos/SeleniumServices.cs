using Dominio.Infraestrutura.Interfaces.Servicos;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Dominio.Servicos
{
    public class SeleniumServices : ISeleniumServices
    {
        public IWebDriver NavegarPagina(string url)
        {
            if(url == null)
                throw new ArgumentNullException("Url não pode ser nula");

            if (!url.Contains("http"))
                throw new UriFormatException("url não contem protocolo para navegação");

            IWebDriver driver = new ChromeDriver();
            
            driver.Navigate().GoToUrl(new Uri(url));

            return driver;
        }

        public IWebDriver PesquisaPagina(IWebDriver driver, string itemPesquisa)
        {
            if (driver == null)
                throw new Exception("Driver esta nulo");


            IWebElement search = driver.FindElement(By.Id("header-barraBusca-form-campoBusca"));

            if(search != null)
            {
                search.SendKeys(itemPesquisa);

                IWebElement searchButton = driver.FindElement(By.ClassName("header-barraBusca-form-submit"));

                searchButton.Click();

                return driver;
            }
            else
            {
                throw new Exception("WebElement search não encontrado");
            }

        }
    }
}
