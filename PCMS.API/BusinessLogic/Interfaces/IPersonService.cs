using PCMS.API.BusinessLogic.Models.Enums;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.BusinessLogic.Interfaces
{
    /// <summary>
    /// Business logic contract for the person service.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Creates a <see cref="Models.Person"/>.
        /// </summary>
        /// <param name="request">The data to create the person.</param>
        /// <returns>The created person.</returns>
        Task<PersonDto> CreatePersonAsync(CreatePersonDto request);

        /// <summary>
        /// Retrieves a person by their ID.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>The person or null if they do not exist.</returns>
        Task<PersonDto?> GetPersonByIdAsync(string personId);

        /// <summary>
        /// Updates a person by their ID.
        /// </summary>
        /// <param name="request">The new data for the update.</param>
        /// <returns>True if the update was successful, false if the person does not exist.</returns>
        Task<bool> UpdatePersonByIdAsync(UpdatePersonDto request);

        /// <summary>
        /// Deletes a person by their ID.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>True if the deletion was successful, false if the person does not exist.</returns>
        Task<bool> DeletePersonByIdAsync(string personId, string userId);

        /// <summary>
        /// Searches for people based on parameters.
        /// </summary>
        /// <returns>A list of people matching the search criteria.</returns>
        Task<List<PersonDto>> SearchPersonsAsync();

        /// <summary>
        /// Links a person to a case.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>True if they were linked, false if either the person or case does not exist or they are already linked.</returns>
        Task<bool> AddPersonToCaseAsync(string personId, string caseId);

        /// <summary>
        /// Links a person to a crime scene.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="crimeSceneId">The ID of the crime scene.</param>
        /// <param name="role">The role they have in the link.</param>
        /// <returns>True if they were linked, false if either the person or crime scene does not exist.</returns>
        /// <remarks>
        /// A person can be linked to a crime scene multiple times through different roles they have in it.
        /// </remarks>
        Task<bool> AddPersonToCrimeSceneAsync(string personId, string crimeSceneId, CrimeSceneRole role);

        /// <summary>
        /// Unlinks a person from a case.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>True if they were unlinked, false if either the person or case does not exist or they were not linked to begin with.</returns>
        Task<bool> DeletePersonFromCaseAsync(string personId, string caseId);

        /// <summary>
        /// Unlinks all links a person has to a crime scene.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="crimeSceneId">The ID of the crime scene.</param>
        /// <returns>True if all links were removed, false if either the person or crime scene does not exist or there were no links to unlink.</returns>
        /// <remarks>
        /// A person can be linked to a crime scene multiple times with different roles for those links. This will remove all links they have to this crime scene.
        /// </remarks>
        Task<bool> DeleteAllPersonCrimeSceneLinks(string personId, string crimeSceneId);

        /// <summary>
        /// Deletes a specific link a person has to a crime scene.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="crimeSceneId">The ID of the crime scene.</param>
        /// <param name="linkId">The ID of the specific link.</param>
        /// <returns>True if it was deleted, false if the models do not exist.</returns>
        Task<bool> DeletePersonCrimeSceneLink(string personId, string crimeSceneId, string linkId);
    }
}
