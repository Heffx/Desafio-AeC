using Dominio.Entidades;
using Microsoft.Data.Sqlite;

namespace Dominio.Infraestrutura.Interfaces.Integracoes
{
    public interface IIntegracaoSqLiteDb
    {
        public SqliteConnection CreateConnection();

        public void CreateTable(SqliteConnection conn);

        public void InsertData(SqliteConnection conn, string command);

        public void ReadData(SqliteConnection conn);

        public void ClosedConnection(SqliteConnection conn);
    }
}
