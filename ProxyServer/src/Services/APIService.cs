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


    public async Task<string> getMagicSchools()
    {
        var response = await _httpClient.GetStringAsync($"{baseURL}/magic-schools");
        return response;
    }

    public async Task<string> getDnDClasses()
    {
        var response = await _httpClient.GetStringAsync($"{baseURL}/classes");
        return response;
    }

    public async Task<string> getSpellListByClass(string school, int? level, string classType)
    {

        var query = $"classes/{classType}/spells";
        var response = await _httpClient.GetStringAsync($"{baseURL}/{query}");
        return response;
    }

    public async Task<string> getSpellList(string school, int? level)
    {
        var query = "spells?";
        query += !string.IsNullOrEmpty(school) ? $"school={school}&" : "";
        query += level.HasValue ? $"level={level.Value}&" : "";
        var response = await _httpClient.GetStringAsync($"{baseURL}/{query}");
        return response;
    }


    public async Task<string> getSpell(string index)
    {
        var query = $"spells/{index}";
        var response = await _httpClient.GetStringAsync($"{baseURL}/{query}");
        return response;
    }

    public string Compare(string spellsByClass, string spellsBySchool)
    {
        var spellListClass = JsonConvert.DeserializeObject<SpellList>(spellsByClass);
        var spellListSchool = JsonConvert.DeserializeObject<SpellList>(spellsBySchool);

        var overlappingSpells = spellListClass.results
            .Where(spell => spellListSchool.results.Any(spell2 => spell2.index == spell.index))
            .ToList();
        var overlappingSpellList = new SpellList(overlappingSpells);
        return JsonConvert.SerializeObject(overlappingSpellList);
    }

    /*
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{baseURL}/{query}");
    request.Headers.Add("Accept", "application/json");
    var response = await _httpClient.SendAsync(request);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadAsStringAsync();
    */
}
