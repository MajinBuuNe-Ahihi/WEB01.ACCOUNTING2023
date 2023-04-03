using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Configurations;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;

namespace WEB01.ACCOUNTING2023.INFRASTRUCTURE.Respository
{
    public class MySQLRepository : IDatabase
    {
        private readonly MySqlConnection _connect;

        public MySQLRepository(DataBaseConfiguration configuration)
        {
            this._connect = new MySqlConnection(connectionString: configuration.ConnectionString);
        }

        public void Close()
        {
            _connect.Close();
        }

        public DbConnection GetConnection()
        {
            return _connect;
        }

        public void Open()
        {
            _connect.Open();
        }

        public List<string> GetVaribleProc(string procName)
        {
            var proc = new List<string>();
            _connect.Open();
            MySqlCommand cmd = new MySqlCommand(procName, _connect);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlCommandBuilder.DeriveParameters(cmd);
            MySqlParameterCollection parameters = cmd.Parameters;
            foreach (MySqlParameter parameter in parameters)
            {
                // Output the name and data type of each parameter
                proc.Add(parameter.ParameterName);
            }
            _connect.Close();
            return proc;
        }

        ~MySQLRepository()
        {
            _connect.Dispose();
        }
    }
}
