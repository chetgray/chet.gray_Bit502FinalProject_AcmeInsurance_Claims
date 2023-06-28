using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AcmeInsurance.Claims.Data.DataAccess
{
    /// <inheritdoc />
    internal class Dal : IDal
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dal"/> class with the passed <paramref
        /// name="connectionString"/>.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to use for any <see cref="SqlConnection"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the passed <paramref name="connectionString"></paramref> is <see
        /// langword="null"/>.
        /// </exception>
        public Dal(string connectionString)
        {
            _connectionString =
                connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <inheritdoc/>
        public void ExecuteStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        )
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (
                SqlCommand command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                }
            )
            {
                connection.Open();
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public IList<object[]> GetRecordListFromStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        )
        {
            IList<object[]> records = new List<object[]>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            object[] record = new object[reader.FieldCount];
                            reader.GetValues(record);
                            for (int i = 0; i < record.Length; i++)
                            {
                                if (record[i] == DBNull.Value)
                                {
                                    record[i] = null;
                                }
                            }

                            records.Add(record);
                        }
                    }
                }
            }

            return records;
        }

        /// <inheritdoc/>
        public DataTable GetTableFromStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        )
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (
                SqlCommand command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                }
            )
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                // Get the first DataTable in the result DataSet
                adapter.Fill(resultTable);
            }

            return resultTable;
        }

        /// <inheritdoc/>
        public object GetValueFromStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        )
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (
                SqlCommand command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                }
            )
            {
                connection.Open();
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                return command.ExecuteScalar();
            }
        }
    }
}
