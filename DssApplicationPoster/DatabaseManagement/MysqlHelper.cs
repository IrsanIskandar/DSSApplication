using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DssApplicationPoster.DatabaseManagement
{
    public class MysqlHelper<T>
    {
        private static MySqlConnection _existingConnection;
        private static MySqlConnection _Connection => _existingConnection ?? (_existingConnection = GetConnection());

        public static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
            conn.Open();

            return conn;
        }

        public async static Task<IEnumerable<T>> ExecuteProcedureAsync(string spName, object param = null)
        {
            IEnumerable<T> result = new List<T>();

            result = await _Connection.QueryAsync<T>(sql: spName, param: param, commandType: CommandType.StoredProcedure);
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            return result;
        }

        public async static Task<IEnumerable<T>> ExecuteQueryAsync(string query, object param = null)
        {
            IEnumerable<T> result = new List<T>();

            result = await _Connection.QueryAsync<T>(sql: query, param: param, commandType: CommandType.Text);
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            return result;
        }

        public static IEnumerable<T> ExecuteQuery(string query, object param = null)
        {
            IEnumerable<T> result = new List<T>();

            result = _Connection.Query<T>(sql: query, param: param, commandType: CommandType.Text);
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            return result;
        }

        public async static Task<T> ExecuteProcedureSingleAsync(string spName, object param = null)
        {
            T result = await _Connection.QueryFirstOrDefaultAsync<T>(sql: spName, param: param, commandType: CommandType.StoredProcedure);
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            return result;
        }

        public async static Task<T> ExecuteQuerySingleAsync(string query, object param = null)
        {
            T result = await _Connection.QueryFirstOrDefaultAsync<T>(sql: query, param: param, commandType: CommandType.Text);
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            return result;
        }

        public static T ExecuteSingle(string query, object param = null)
        {
            T result = _Connection.QueryFirstOrDefault<T>(sql: query, param: param, commandType: CommandType.Text);
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            return result;
        }

        public async static Task ExecutePocedureNoReturnAsync(string spName, object param = null)
        {
            await _Connection.ExecuteAsync(sql: spName, param: param, commandType: CommandType.StoredProcedure);
            _Connection.Close();
        }

        public async static Task ExecuteQueryNoReturnAsync(string query, object param = null)
        {
            await _Connection.ExecuteAsync(sql: query, param: param, commandType: CommandType.Text);
            _Connection.Close();
        }

        public static void ExecuteNoReturn(string query, object param = null)
        {
            _Connection.ExecuteAsync(sql: query, param: param, commandType: CommandType.Text);
            _Connection.Close();
        }
    }

    public class MysqlHelper : IDisposable
    {
        public MySqlConnection Connection;

        private static MySqlConnection _existingConnection;
        private static MySqlConnection _mysqlConn => _existingConnection ?? (_existingConnection = GetConnection());

        public MysqlHelper(string connection)
        {
            Connection = new MySqlConnection(connection);
        }

        private static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(Constants.ConnsStrings);
            connection.Open();

            return connection;
        }

        public void Dispose()
        {
            _mysqlConn.Dispose();
        }
    }
}