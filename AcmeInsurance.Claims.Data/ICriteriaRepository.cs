using System.Collections.Generic;

using AcmeInsurance.Claims.Data.Objects;

namespace AcmeInsurance.Claims.Data
{
    /// <summary>Provides data access operations for the criteria records.</summary>
    public interface ICriteriaRepository
    {
        /// <summary>Gets a list of all criteria records.</summary>
        /// <returns>
        /// A <see cref="IList"/> of <see cref="ICriteriaDto"/>s for all criteria records.
        /// </returns>
        IList<ICriteriaDto> ListAll();
    }
}
