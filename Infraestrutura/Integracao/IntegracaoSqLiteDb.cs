using Dominio.Entidades;
using Dominio.Infraestrutura.Interfaces.Integracoes;
using Microsoft.Data.Sqlite;

namespace Infraestrutura.Integracao
{
    public class IntegracaoSqLiteDb : IIntegracaoSqLiteDb
    {
        public SqliteConnection CreateConnection()
        {
            SqliteConnection sqlite_conn;

            sqlite_conn = new SqliteConnection($"Data Source={Environment.CurrentDirectory}\\database.db; Cache  = Shared; Mode = ReadWriteCreate;");
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                throw new SqliteException($"Erro ao conectar no banco {ex}", 404);
            }
            return sqlite_conn;
        }

        public void CreateTable(SqliteConnection conn)
        {

            SqliteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE Curso ([Nome][varchar](255),[Descricao] [varchar] (255),[CargaHoraria][varchar] (255),[Professor][varchar] (255))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        public void InsertData(SqliteConnection conn, string command)
        {
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = command;
            sqlite_cmd.ExecuteNonQuery();
        }

        public void ReadData(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Curso";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string nome = sqlite_datareader?.GetString(0);
                Console.WriteLine(nome);
                string descricao = sqlite_datareader?.GetString(1);
                Console.WriteLine(descricao);
                string cargaHoraria = sqlite_datareader?.GetString(2);
                Console.WriteLine(cargaHoraria);
                string professor = sqlite_datareader?.GetString(3);
                Console.WriteLine(professor);
            }
        }

        public void ClosedConnection(SqliteConnection conn)
        {
            conn.Close();
        }
    }
}
