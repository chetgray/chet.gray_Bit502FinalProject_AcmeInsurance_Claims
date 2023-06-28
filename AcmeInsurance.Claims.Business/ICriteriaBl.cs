using System.Collections.Generic;

using AcmeInsurance.Claims.Models;

namespace AcmeInsurance.Claims.Business
{
    /// <summary>Provides business logic operations for criteria records.</summary>
    public interface ICriteriaBl
    {
        /// <summary>Gets a list of all criteria records.</summary>
        /// <returns>
        /// A <see cref="IList"/> of <see cref="ICriteriaModel"/>s for all criteria records.
        /// </returns>
        IList<ICriteriaModel> ListAll();
    }
}
