using System.Text;
using System.Text.Json;
using MyGarden.Models;

namespace MyGarden.Web.Services;

public class ToDoService
{
    private readonly HttpClient _httpClient;

    public ToDoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ToDo>> GetAll()
    {
        var result = await _httpClient.GetAsync("todos");
        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ToDo>>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return new List<ToDo>();
    }

    public async Task<ToDo> Get(string id)
    {
        var result = await _httpClient.GetAsync($"todos/{id}");
        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ToDo>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return null;
    }
}