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

        var result = await _apiService.getMagicSchools();

        Assert.NotNull(result);
        Assert.Contains("abjuration", result);
    }

    [Fact]
    public void CompareJSONTest()
    {
        string byClass = @"{""count"":2,""results"":[{""index"":""druidcraft"",""name"":""Druidcraft"",""level"":0,""url"":""/api/spells/druidcraft""},{""index"":""guidance"",""name"":""Guidance"",""level"":0,""url"":""/api/spells/guidance""}]}";
        string bySchool = @"{""count"":1,""results"":[{""index"":""druidcraft"",""name"":""Druidcraft"",""level"":0,""url"":""/api/spells/druidcraft""}]}";
        var overlap = _apiService.Compare(byClass, bySchool);
        Assert.Equal(bySchool, overlap);
    }
}