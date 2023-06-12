using Dominio.Entidades;

namespace Dominio.Infraestrutura.Interfaces.Servicos
{
    public interface ISeleniumServices
    {
        public void NavegarPagina(string url);

        public void PesquisaPagina(string itemPesquisa);

        public List<Curso> BuscarConteudodeCursos();

        public void FiltrarPorTipo(string tipo);

        public void EncerrarDriver();
    }
}
