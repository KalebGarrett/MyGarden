using System.Text;
using System.Text.Json;
using MyGarden.Models;

namespace MyGarden.Web.Services;

public class PlantService
{
    private readonly HttpClient _httpClient;

    public PlantService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Plant>> GetAll()
    {
        var result = await _httpClient.GetAsync("plants");
        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Plant>>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return new List<Plant>();
    }

    public async Task<Plant> Get(string id)
    {
        var result = await _httpClient.GetAsync($"plants/{id}");
        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Plant>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return null;
    }
    
    public async Task<Plant> Create(Plant plant)
    {
        var json = JsonSerializer.Serialize(plant);
        var result = await _httpClient.PostAsync("plants", new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        return plant;
    }
    
      
    public async Task<Plant> Update(string id, Plant plant)
    {
        var json = JsonSerializer.Serialize(plant);
        var result = await _httpClient.PutAsync($"plants/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }
        return plant;
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _httpClient.DeleteAsync($"plants/{id}");
        return result.IsSuccessStatusCode;
    }
}