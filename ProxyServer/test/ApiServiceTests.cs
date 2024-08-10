using Moq;
using Moq.Contrib.HttpClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
namespace test;

public class ApiServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHandler;
    private readonly HttpClient _httpClient;
    private readonly ApiService _apiService;
    public ApiServiceTests()
    {
        _mockHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHandler.Object)
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/api")
        };
        _apiService = new ApiService(_httpClient);
    }
    [Fact]
    public async Task GetMagicSchoolsTest()
    {
        var responseContent = @"{""count"":1,""results"":[{""index"":""abjuration"",""name"":""Abjuration"",""url"":""/api/magic-schools/abjuration""}]}";
        _mockHandler.SetupRequest(HttpMethod.Get, "https://www.dnd5eapi.co/api/magic-schools")
                    .ReturnsResponse(responseContent);

        var result = await _apiService.GetData("magic-schools");

        Assert.NotNull(result);
        Assert.Contains("abjuration", result);
        
    }

    [Fact]
    public void IntersectJSONTest()
    {
        string spellsByClass = @"{""count"":2,""results"":[{""index"":""druidcraft"",""name"":""Druidcraft"",""level"":0,""url"":""/api/spells/druidcraft""},{""index"":""guidance"",""name"":""Guidance"",""level"":0,""url"":""/api/spells/guidance""}]}";
        string spellsBySchool = @"{""count"":1,""results"":[{""index"":""druidcraft"",""name"":""Druidcraft"",""level"":0,""url"":""/api/spells/druidcraft""}]}";
        var overlap = _apiService.IntersectJSONSpellLists(spellsByClass, spellsBySchool);
        Assert.Equal(spellsBySchool, overlap);
    }

    [Fact]
    public void NoIntersectJSONTest()
    {
        string spellsByClass = @"{""count"":2,""results"":[{""index"":""acid-arrow"",""name"":""Acid Arrow"",""level"":2,""url"":""/api/spells/acid-arrow""},{""index"":""guidance"",""name"":""Guidance"",""level"":0,""url"":""/api/spells/guidance""}]}";
        string spellsBySchool = @"{""count"":1,""results"":[{""index"":""druidcraft"",""name"":""Druidcraft"",""level"":0,""url"":""/api/spells/druidcraft""}]}";
        string expected = @"{""count"":0,""results"":[]}";
        var overlap = _apiService.IntersectJSONSpellLists(spellsByClass, spellsBySchool);
        Assert.Equal(expected, overlap);
    }
}