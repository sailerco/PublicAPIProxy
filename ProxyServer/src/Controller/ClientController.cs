using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace ProxyServer.Controller
{
    /// <summary> Provides endpoints for interacting with the <see cref="ApiService"/> to fetch and compare data.</summary>
    [ApiController]
    [Route("api")]
    public class ClientController : ControllerBase
    {

        private readonly ApiService _apiService;

        public ClientController(ApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary> Forwards a GET request to the specified endpoint of the external API and returns the data. </summary>
        /// <param name="endpoint">The API endpoint to query, such as "classes" or "magic-schools".</param>
        /// <returns>An <see cref="IActionResult"/> containing the data from the external API or an error response if the request fails.</returns>
        [HttpGet("{endpoint}")]
        public async Task<IActionResult> FetchData(string endpoint)
        {
            var data = await _apiService.GetData(endpoint);
            return Ok(data);
        }

        /// <summary> Forwards a GET request to the specified endpoint of the external API with an additional index parameter and returns the data. </summary>
        /// <param name="endpoint">The API endpoint to query, such as "spells".</param>
        /// <param name="index">The specific index or identifier to query within the endpoint, such as a specific spell</param>
        /// <returns>An <see cref="IActionResult"/> containing the data from the external API or an error response if the request fails.</returns>
        [HttpGet("{endpoint}/{index}")]
        public async Task<IActionResult> FetchSpell(string endpoint, string index)
        {
            var data = await _apiService.GetData($"{endpoint}/{index}");
            return Ok(data);
        }

        /// <summary> Fetches a list of spells based on various filter criteria and returns the data.</summary>
        /// <param name="magicSchool">The magicSchool of magic to filter spells by, e.g., "evocation". If empty, no filtering by magicSchool is applied.</param>
        /// <param name="level">The spell level to filter by. If not specified, no filtering by level is applied.</param>
        /// <param name="classType">The class type to filter spells by, e.g., "wizard". If empty, no filtering by class is applied.</param>
        /// <returns>An <see cref="IActionResult"/> containing the filtered list of spells or a comparison of lists if both class and magicSchool filters are provided.</returns>
        [HttpGet("spellList")]
        public async Task<IActionResult> FetchSpellList(string magicSchool = "", int? level = null, string classType = "")
        {
            string? spells;
            if (!string.IsNullOrEmpty(classType) && string.IsNullOrEmpty(magicSchool) && !level.HasValue)
            {
                spells = await _apiService.getSpellListByClass(classType);
                return Ok(spells);
            }
            else if (string.IsNullOrEmpty(classType))
            {
                spells = await _apiService.getSpellList(magicSchool, level);
                return Ok(spells);
            }
            else
            {
                var spellsByClass = await _apiService.getSpellListByClass(classType);
                var spellsBySchool = await _apiService.getSpellList(magicSchool, level);
                spells = _apiService.Compare(spellsByClass, spellsBySchool);
                return Ok(spells);
            }
        }
    }
}