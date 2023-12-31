﻿using System.Collections.Generic;

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

        /// <summary>Adds the records in the specified collection.</summary>
        /// <param name="dtos">
        /// The collection whose elements records should be added.
        /// </param>
        /// <returns>
        /// A <see cref="IList"/> of <see cref="ICriteriaDto"/>s for the added records, with
        /// their <see cref="ICriteriaDto.Id"/>s set.
        /// </returns>
        IList<ICriteriaDto> AddRange(IEnumerable<ICriteriaDto> dtos);

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

        /// <summary>Removes the record with the specified ID.</summary>
        /// <param name="id">The ID of the record to remove.</param>
        /// <returns>
        /// <see langword="true"/> if the record was successfully removed; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool RemoveById(int id);

        /// <summary>Updates the specified criteria record.</summary>
        /// <param name="dto">The criteria record to update.</param>
        /// <returns>The updated criteria record.</returns>
        ICriteriaDto Update(ICriteriaDto dto);
    }
}
