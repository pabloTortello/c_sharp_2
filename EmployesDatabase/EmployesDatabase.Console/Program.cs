using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployesDatabase.ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string connection_string =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var str = ConfigurationManager.ConnectionStrings["DefaultConnection"];

            using (var connection = new SqlConnection(str.ConnectionString))
            {
                connection.Open();
            }

            //var builder = new SqlConnectionStringBuilder(connection_string);

            //SqlConnection connection = null;
            //try
            //{
            //    connection = new SqlConnection(connection_string);
            //    connection.Open();

            //    // ...
            //}
            //finally
            //{
            //    connection?.Dispose();
            //}

            //using (var connection = new SqlConnection(connection_string))
            //{
            //    connection.Open();

            //    var command = new SqlCommand("", connection);
            //}

            //ExecuteNonQuery(connection_string, "TestTable");
            //var employes_count = (int)ExecuteScalar(connection_string);
            //var inserted_id = ExecuteScalar(connection_string, "Иванов", "11.12.12", "qwerty@mail.ru", "+7(916)111-22-33");

            //ExecuteReader(connection_string);


            //const string birthday = "05.12.07";
            //foreach (var employee in GetEmployes(connection_string, birthday))
            //{
            //    Console.WriteLine(employee);
            //}


            //SqlDataAdapter adapter;
            //DataTable table;
            //DataSet data_set;

            DataAdapterTest(connection_string);

            Console.ReadLine();
        }

        private static void ExecuteNonQuery(string ConnectionString, string TableName)
        {
            var sql = $"CREATE TABLE [dbo].[{TableName}] ([Id] INT NOT NULL, [Name] NVARCHAR (MAX) NOT NULL, [Birthday] NVARCHAR (MAX) NOT NULL, [Email] NVARCHAR (100) NOT NULL, [Phone] NVARCHAR (MAX) NOT NULL, PRIMARY KEY CLUSTERED ([Id] ASC));";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sql, connection);

                var rows_count = command.ExecuteNonQuery();
            }
        }

        private static object ExecuteScalar(string ConnectionString, string TableName = "Employes")
        {
            var sql_select = $"SELECT COUNT(*) FROM {TableName}";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sql_select, connection);

                return command.ExecuteScalar();
            }
        }

        private static int ExecuteScalar(string ConnectionString, string Name, string Birthday, string Email, string Phone)
        {
            var sql_insert = $@"INSERT INTO Employes (Name, Birthday, Email, Phone) 
OUTPUT INSERTED.ID
VALUES (N'{Name}', '{Birthday}', N'{Email}', N'{Phone}')";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sql_insert, connection);

                return (int)command.ExecuteScalar();
            }
        }

        private static void ExecuteReader(string ConnectionString)
        {
            var sql_select = "SELECT * FROM Employes";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sql_select, connection);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows) return;

                    while (reader.Read())
                    {
                        var id = (int)reader.GetValue(0);
                        var name = reader.GetString(1);
                        var birthday = (string)reader["Birthday"];
                        var email_col_index = reader.GetOrdinal("Email");
                        var email = reader.GetString(email_col_index);
                    }
                    //reader.Close();
                }
            }
        }

        private static IEnumerable<(string Name, string Email, string Phone)> GetEmployes(string ConnectionString, string Birthday)
        {
            var sql_select = "SELECT * FROM Employes WHERE Birthday = @Birthday";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sql_select, connection);
                var parameter = command.Parameters.Add("Birthday", SqlDbType.NVarChar, -1);
                parameter.Value = Birthday;

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows) yield break;

                    while (reader.Read())
                    {
                        var id = (int)reader.GetValue(0);
                        var name = reader.GetString(1);
                        var birthday = (string)reader["Birthday"];
                        var email_col_index = reader.GetOrdinal("Email");
                        var email = reader.GetString(email_col_index);
                        var phone = (string)reader["Phone"];

                        yield return (name, email, phone);
                    }
                    //reader.Close();
                }
            }
        }

        private static void DataAdapterTest(string ConnectionString)
        {
            var sql_select = "SELECT * FROM Employes";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var adapter = new SqlDataAdapter(sql_select, connection);
                var sql_insert = @"INSERT INTO Employes (Name, Birthday, Email, Phone)
VALUES (@Name, @Birthday, @Email, @Phone); SET @ID = @@IDENTITY;";
                var insert_command = new SqlCommand(sql_insert, connection);
                insert_command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
                insert_command.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "Birthday");
                insert_command.Parameters.Add("@Email", SqlDbType.NVarChar, -1, "Email");
                insert_command.Parameters.Add("@Phone", SqlDbType.NVarChar, -1, "Phone");
                insert_command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").Direction = ParameterDirection.Output;

                adapter.InsertCommand = insert_command;

                adapter.TableMappings.Add("Table", "Employes");

                var table = new DataTable();
                adapter.Fill(table);
                //table.Rows[0]["Phone"];

                foreach (DataRow row in table.Rows)
                {
                    foreach (var cell in row.ItemArray)
                    {
                        Console.Write(cell);
                        Console.Write(", ");
                    }
                    Console.WriteLine();
                }

                var new_row = table.NewRow();
                new_row["Name"] = "Alexander";
                new_row["Birthday"] = "11.04.83";
                new_row["Email"] = "qqq@fff.ru";
                new_row["Phone"] = "777";
                table.Rows.Add(new_row);

                adapter.Update(table);

                Console.WriteLine("-------------");

                var builder = new SqlCommandBuilder(adapter);
                builder.RefreshSchema();

                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.UpdateCommand = builder.GetUpdateCommand();

                var data = new DataSet();
                adapter.Fill(data);

                foreach (DataTable t in data.Tables)
                {
                    Console.WriteLine(t.TableName);
                    foreach (DataRow row in t.Rows)
                    {
                        foreach (var cell in row.ItemArray)
                        {
                            Console.Write(cell);
                            Console.Write(", ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
