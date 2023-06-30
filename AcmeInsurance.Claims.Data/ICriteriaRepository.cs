using System.Collections.Generic;

using AcmeInsurance.Claims.Data.Objects;

namespace AcmeInsurance.Claims.Data
{
    /// <summary>Provides data access operations for the criteria records.</summary>
    public interface ICriteriaRepository
    {
        /// <summary>Adds a new criteria record.</summary>
        /// <param name="dto">The criteria record to add.</param>
        /// <returns>
        /// The newly added criteria record, with the <see cref="ICriteriaDto.Id"/> set.
        /// </returns>
        ICriteriaDto Add(ICriteriaDto dto);

        /// <summary>Gets a criteria record with the specified ID.</summary>
        /// <param name="id">The ID of the criteria record to retrieve.</param>
        /// <returns>
        /// A <see cref="ICriteriaDto"/> of the criteria record, or <see langword="null"/> if
        /// none exists.
        /// </returns>
        ICriteriaDto GetById(int id);

        /// <summary>Gets a list of all criteria records.</summary>
        /// <returns>
        /// A <see cref="IList"/> of <see cref="ICriteriaDto"/>s for all criteria records.
        /// </returns>
        IList<ICriteriaDto> ListAll();
    }
}
