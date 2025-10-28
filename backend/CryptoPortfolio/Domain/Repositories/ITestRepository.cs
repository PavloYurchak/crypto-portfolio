// <copyright file="ITestRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// Represents a test repository.
    /// </summary>
    public interface ITestRepository : IDomainRepository
    {
        /// <summary>
        /// Gets the tests.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A collection of tests.</returns>
        Task<IEnumerable<Test>> GetTests(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the test by identifier.
        /// </summary>
        /// <param name="id">The identifier of the test to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The test with the specified identifier, or null if not found.</returns>
        Task<Test?> GetTestById(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new test.
        /// </summary>
        /// <param name="test">The test to create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The created test, or null if creation failed.</returns>
        Task<Test?> CreateTest(Test test, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing test.
        /// </summary>
        /// <param name="test">The test to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The updated test, or null if the update failed.</returns>
        Task<Test?> UpdateTest(Test test, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a test by identifier.
        /// </summary>
        /// <param name="testIds">The identifier of the test to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A boolean indicating whether the test was successfully deleted.</returns>
        Task<bool> DeleteTest(IEnumerable<int> testIds, CancellationToken cancellationToken = default);
    }
}
