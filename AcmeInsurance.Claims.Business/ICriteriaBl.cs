using System.Collections.Generic;

using AcmeInsurance.Claims.Models;

namespace AcmeInsurance.Claims.Business
{
    /// <summary>Provides business logic operations for criteria records.</summary>
    public interface ICriteriaBl
    {
        /// <summary>Adds a new criteria record.</summary>
        /// <param name="model">The criteria record to add.</param>
        /// <returns>
        /// The newly added criteria record, with the <see cref="ICriteriaModel.Id"/> set.
        /// </returns>
        ICriteriaModel Add(ICriteriaModel model);

        /// <summary>Gets a criteria record with the specified ID.</summary>
        /// <param name="id">The ID of the criteria record to retrieve.</param>
        /// <returns>
        /// A <see cref="ICriteriaModel"/> of the criteria record, or <see langword="null"/> if
        /// none exists.
        /// </returns>
        ICriteriaModel GetById(int id);

        /// <summary>Gets a list of all criteria records.</summary>
        /// <returns>
        /// A <see cref="IList"/> of <see cref="ICriteriaModel"/>s for all criteria records.
        /// </returns>
        IList<ICriteriaModel> ListAll();

        /// <summary>Removes the record with the specified ID.</summary>
        /// <param name="id">The ID of the record to remove.</param>
        /// <returns>
        /// <see langword="true"/> if the record was successfully removed; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool RemoveById(int id);

        /// <summary>Updates the specified criteria record.</summary>
        /// <param name="model">The criteria record to update.</param>
        /// <returns>The updated criteria record.</returns>
        ICriteriaModel Update(ICriteriaModel model);
    }
}
