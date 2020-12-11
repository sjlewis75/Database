using System;
using Npgsql;

namespace Database
{
    public class DatabaseCommands
    {
        public static NpgsqlConnection ConnectToDb()
        {

            var connectionString = GetDbConnectionStr();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }


        private static string GetDbConnectionStr()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../../fdps.dat");          
            string  parameter = file.ReadLine();
            return parameter;
        }

        public static NpgsqlConnection ConnectToDb(string connectionStr)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionStr);
            connection.Open();
            return connection;
        }

        public static void DisconnectFromDb(NpgsqlConnection connection)
        {
            connection.Dispose();
            connection.Close();
        }

        public static void RunDbCommandWithConnection(string queryString, NpgsqlConnection connectionStr)
        {
            var connection = ConnectToDb();

            NpgsqlCommand cmd = new NpgsqlCommand
            {
                Connection = connection,
                CommandType = System.Data.CommandType.Text,
                CommandText = queryString
            };

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                DisconnectFromDb(connection);
            }
        }

        public static int RunNonQueryCommand(string queryString, NpgsqlConnection connectionStr)
        {

            NpgsqlCommand cmd = new NpgsqlCommand
            {
                Connection = connectionStr,
                CommandType = System.Data.CommandType.Text,
                CommandText = queryString
            };

            try
            {
                var retCode = cmd.ExecuteNonQuery();
                return retCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        public static int ExecuteRowCountCommand(string queryString, NpgsqlConnection connectionStr)
        {

            NpgsqlCommand cmd = new NpgsqlCommand
            {
                Connection = connectionStr,
                CommandType = System.Data.CommandType.Text,
                CommandText = queryString
            };

            try
            {
                var retCode = cmd.ExecuteReader();
                if (retCode.HasRows)
                {
                    retCode.Close();
                    return 1;
                }
                else
                {
                    retCode.Close();
                    return -1;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static int ExecuteRowCountCommand( NpgsqlCommand cmd)
        {

            try
            {
                var retCode = cmd.ExecuteReader();
                if (retCode.HasRows)
                {
                    retCode.Close();
                    return 1;
                }
                else
                {
                    retCode.Close();
                    return -1;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
