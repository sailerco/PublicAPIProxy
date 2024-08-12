using System.Diagnostics;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string baseURL = "https://www.dnd5eapi.co/api";

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary> Retrieves data from the specified API endpoint. </summary>
    /// <param name="query">The API endpoint to query, e.g., "classes" or "magic-schools".</param>
    /// <returns>A task result which contains the API response as a JSON string.</returns>
    public async Task<string> GetData(string query)
    {
        var response = await _httpClient.GetStringAsync($"{baseURL}/{query}");
        return response;
    }

    /// <summary> Retrieves a list of spells for a specific class from the API. </summary>
    /// <param name="classType">The class type for which to retrieve spells, e.g., "wizard" or "cleric".</param>
    /// <returns>A task result which contains the list of spells as a JSON string.</returns>
    public async Task<string> GetSpellListByClass(string classType)
    {
        var query = $"classes/{classType}/spells";
        var response = await _httpClient.GetStringAsync($"{baseURL}/{query}");
        return response;
    }

    /// <summary> Retrieves a list of spells based on the specified school and level.</summary>
    /// <param name="school">The school of magic to filter spells by, e.g., "evocation" or "illusion".</param>
    /// <param name="level">The spell level to filter by, or null to include all levels.</param>
    /// <returns>A task result which contains the list of spells as a JSON string.</returns>
    public async Task<string> GetSpellList(string school, int? level)
    {
        var query = "spells?";
        query += !string.IsNullOrEmpty(school) ? $"school={school}&" : "";
        query += level.HasValue ? $"level={level.Value}&" : "";
        var response = await _httpClient.GetStringAsync($"{baseURL}/{query}");
        return response;
    }

    /// <summary> Compares two lists of spells to find overlapping spells.</summary>
    /// <param name="spellsByClass">A string representing the list of spells by class.</param>
    /// <param name="spellsBySchool">A string representing the list of spells by school.</param>
    /// <returns>A JSON string representing the list of spells that appear in both provided lists.</returns>
    public string IntersectJSONSpellLists(string spellsByClass, string spellsBySchool)
    {
        var spellListClass = JsonConvert.DeserializeObject<SpellList>(spellsByClass);
        var spellListSchool = JsonConvert.DeserializeObject<SpellList>(spellsBySchool);

        var overlappingSpells = spellListClass.results
            .Where(classSpell => spellListSchool.results.Any(schoolSpell => schoolSpell.index == classSpell.index))
            .ToList();
        var overlappingSpellList = new SpellList(overlappingSpells);
        return JsonConvert.SerializeObject(overlappingSpellList);
    }
}
