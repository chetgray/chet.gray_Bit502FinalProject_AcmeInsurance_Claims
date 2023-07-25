using System.Collections.Generic;
using System.Data;

namespace AcmeInsurance.Claims.Data.DataAccess
{
    public interface IDal
    {
        /// <summary>Executes a stored procedure with parameters.</summary>
        /// <param name="storedProcedureName">
        /// The name of the stored procedure to execute.
        /// </param>
        /// <param name="parameters">
        /// A dictionary of the parameters to pass to the stored procedure.
        /// </param>
        void ExecuteStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        );

        /// <inheritdoc cref="ExecuteStoredProcedure"/>
        /// <summary>
        /// Executes a stored procedure with parameters, returning a single record.
        /// </summary>
        /// <returns>
        /// A <see cref="object[]"/> array representing the record, or <see langword="null"/> if
        /// none was returned.
        /// </returns>
        object[] GetRecordFromStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        );

        /// <inheritdoc cref="ExecuteStoredProcedure"/>
        /// <summary>
        /// Executes a stored procedure with parameters, returning a list of records.
        /// </summary>
        /// <returns>
        /// A <see cref="IList"/> of records, where each record is an <see cref="object[]"/>
        /// array.
        /// </returns>
        IList<object[]> GetRecordListFromStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        );

        /// <inheritdoc cref="ExecuteStoredProcedure"/>
        /// <summary>
        /// Executes a stored procedure with parameters, returning a <see cref="DataTable"/>.
        /// </summary>
        /// <returns>
        /// The first <see cref="DataTable"/> in the result set returned from the stored
        /// procedure.
        /// </returns>
        DataTable GetTableFromStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        );

        /// <inheritdoc cref="ExecuteStoredProcedure"/>
        /// <summary>
        /// Executes a stored procedure with parameters, returning a single value.
        /// </summary>
        /// <returns>The single scalar value returned from the stored procedure.</returns>
        object GetValueFromStoredProcedure(
            string storedProcedureName,
            IDictionary<string, object> parameters
        );
    }
}
