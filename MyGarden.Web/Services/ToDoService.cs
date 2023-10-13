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
    
    public async Task<ToDo> Create(ToDo toDo)
    {
        var json = JsonSerializer.Serialize(toDo);
        var result = await _httpClient.PostAsync("todos", new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        return toDo;
    }
    
      
    public async Task<ToDo> Update(string id, ToDo toDo)
    {
        var json = JsonSerializer.Serialize(toDo);
        var result = await _httpClient.PutAsync($"todos/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }
        return toDo;
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _httpClient.DeleteAsync($"todos/{id}");
        return result.IsSuccessStatusCode;
    }
}