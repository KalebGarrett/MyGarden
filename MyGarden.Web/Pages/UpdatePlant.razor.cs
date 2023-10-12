using Microsoft.AspNetCore.Components;
using MyGarden.Models;
using MyGarden.Web.Services;

namespace MyGarden.Web.Pages
{
    public partial class UpdatePlant: ComponentBase
    {
        [Inject]
        public PlantService _PlantService { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Parameter]
        public string Id { get; set; }

        public Plant Plant { get; set; } = new Plant();

        private bool Updated { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            Plant = await _PlantService.Get(Id);
            if (Plant == null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        public async Task UpdatePlantDb()
        {
            Plant = await _PlantService.Update(Id, Plant);

            if (Plant != null)
            {
                Updated = true;
            }
        }
    }
}
