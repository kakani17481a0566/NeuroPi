using SchoolManagement.ViewModel.CountingTestContent;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    /// <summary>
    /// Defines the contract for retrieving counting test content.
    /// </summary>
    public interface ICountingTestContentService
    {
        /// <summary>
        /// Retrieves all counting test content records for a given test.
        /// </summary>
        /// <param name="testId">The ID of the parent test.</param>
        List<CountingTestContentRespounceVM> GetByTestId(int testId);

        /// <summary>
        /// Retrieves a specific counting test content record by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the counting test content.</param>
        CountingTestContentRespounceVM GetById(int id);
    }
}
