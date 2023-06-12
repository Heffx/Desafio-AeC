using Dominio.Entidades;
using Dominio.Infraestrutura.Interfaces.Servicos;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Infraestrutura.Servicos
{
    public class SeleniumServices : ISeleniumServices
    {
        private ChromeDriver _driver;

        public SeleniumServices()
        {
            _driver = new ChromeDriver();
        }

        public void NavegarPagina(string url)
        {
            if(url == null)
                throw new ArgumentNullException("Url não pode ser nula");

            if (!url.Contains("http"))
                throw new UriFormatException("url não contem protocolo para navegação");
                        
            _driver.Navigate().GoToUrl(new Uri(url));
        }

        public void PesquisaPagina(string itemPesquisa)
        {
            new WebDriverWait(_driver, new TimeSpan(0,0,30)).Until(_driver => _driver.FindElement(By.Id("header-barraBusca-form-campoBusca")));

            IWebElement search = _driver.FindElement(By.Id("header-barraBusca-form-campoBusca"));

            if(search != null)
            {
                search.SendKeys(itemPesquisa);

                IWebElement searchButton = _driver.FindElement(By.ClassName("header-barraBusca-form-submit"));

                searchButton.Click();
            }
            else
            {
                throw new Exception("WebElement search não encontrado");
            }
        }

        public void FiltrarPorTipo(string tipo)
        {
            new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).Until(driver => driver.FindElement(By.XPath("//*[@id=\"busca-resultados\"]/ul")));

            var url = _driver.Url;

            url = url + $"&typeFilters={tipo}";

            _driver.Navigate().GoToUrl(url);
        }

        public List<Curso> BuscarConteudodeCursos()
        {
            List<Curso> cursos = new List<Curso>();

            new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).Until(driver => driver.FindElement(By.XPath("//*[@id=\"busca-resultados\"]/ul")));

            var listaConteudo = _driver.FindElement(By.XPath("//*[@id=\"busca-resultados\"]/ul")).FindElements(By.ClassName("busca-resultado-container"));

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(150);

            foreach (var item in listaConteudo)
            {
                new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).Until(driver => driver.FindElement(By.ClassName("busca__title")).Displayed);
                Thread.Sleep(5000);

                var nome = item.FindElement(By.ClassName("busca-resultado-nome"))?.Text;
                var descricao = item.FindElement(By.ClassName("busca-resultado-descricao"))?.Text;

                item.Click();

                new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).Until(driver => driver.FindElement(By.XPath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]")));

                var cargaHoraria = _driver.FindElement(By.XPath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]"))?.Text;
                var professor = _driver.FindElement(By.ClassName("instructor-title--name"))?.Text;

                cursos.Add(new Curso
                {
                    Nome = nome,
                    Descricao = descricao,
                    CargaHoraria = cargaHoraria,
                    Professor = professor
                });

                _driver.Navigate().Back();
            }

            return cursos;
        }
    }
}
