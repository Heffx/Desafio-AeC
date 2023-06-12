namespace Dominio.Infraestrutura.Interfaces.Servicos
{
    public interface ISeleniumServices
    {
        public void NavegarPagina(string url);

        public void PesquisaPagina(string itemPesquisa);

        public void BuscarConteudodeCursos();

        public void FiltrarPorTipo(string tipo);
    }
}
