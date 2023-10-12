using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyGarden.Models;
using MyGarden.Web.Services;

namespace MyGarden.Web.Pages
{
    public partial class AddPlant
    {
        private Plant Plant { get; set; } = new Plant();
        private bool Added { get; set; }
        [Inject]
        private PlantService _PlantService { get; set; }
        
        private async Task AddPlantToDb()
        {
            var addPlant = await _PlantService.Create(Plant);

            if (addPlant != null)
            {
                Added = true;
                Plant = new Plant();
            }
        }
    }
}