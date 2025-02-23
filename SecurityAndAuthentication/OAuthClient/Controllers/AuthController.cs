using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace OAuthClient.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("/callback")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://localhost:5181/api/oauth/token", new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("clientId", "client_app")
            ]));

            string content = await response.Content.ReadAsStringAsync();
            Dictionary<string, object>? tokenResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

            return Content($"Access Token: {tokenResponse?["access_token"]}");
        }
    }
}