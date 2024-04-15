using System.Net.Http.Headers;
using System.Text;

namespace EntegraCaseStudy;

public static class HttpHelper
{
    private static readonly HttpClient _httpClient = new ();

    public static async Task<string> PostAsync(string url, StringContent content)
    {
        var response = await _httpClient.PostAsync(url, content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new Exception($"Error code {response.StatusCode}");
        }
    }


    public static async Task<string> GetAsync(string url, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", token);
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new Exception($"Error code {response.StatusCode}");
        }
    }
}