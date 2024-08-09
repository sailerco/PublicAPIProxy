using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace ProxyServer.Controller
{
    [ApiController]
    [Route("api")]
    public class ClientController : ControllerBase
    {

        private readonly ApiService _apiService;

        public ClientController(ApiService apiService)
        {
            _apiService = apiService;
        }


        [HttpGet("magicSchools")]
        public async Task<IActionResult> FetchMagicSchools()
        {
            var data = await _apiService.getMagicSchools();
            return Ok(data);
        }

        [HttpGet("classes")]
        public async Task<IActionResult> FetchClasses()
        {
            var data = await _apiService.getDnDClasses();
            return Ok(data);
        }

        [HttpGet("spellList")]
        public async Task<IActionResult> FetchSpellList(string school = "", int? level = null, string classType = "")
        {
            if (!string.IsNullOrEmpty(classType) && classType != "All classes")
            {
                var spellsByClass = await _apiService.getSpellListByClass(school, level, classType);
                var spellsBySchool = await _apiService.getSpellList(school, level);
                var spells = _apiService.Compare(spellsByClass, spellsBySchool);
                return Ok(spells);
            }
            else
            {
                var spells = await _apiService.getSpellList(school, level);
                return Ok(spells);
            }
        }

        [HttpGet("spells/{spellName}")]
        public async Task<IActionResult> FetchSpell(string spellName)
        {
            var data = await _apiService.getSpell(spellName);
            return Ok(data);
        }

    }
}