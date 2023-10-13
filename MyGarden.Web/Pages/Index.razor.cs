using Microsoft.AspNetCore.Components;
using MyGarden.Models;
using MyGarden.Web.Services;

namespace MyGarden.Web.Pages
{
    public partial class Index
    {
        [Inject]
        public PlantService _PlantService { get; set; }
        [Inject]
        public ToDoService  _ToDoService{ get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<Plant> Plants { get; set; } = new List<Plant>();
        public List<ToDo> ToDos { get; set; } = new List<ToDo>();
        
        protected override async Task OnInitializedAsync()
        {
            Plants = await _PlantService.GetAll();
            ToDos = await _ToDoService.GetAll();
        }
        
        private async Task DeletePlant(int plantId)
        {
            var success = await _PlantService.Delete(plantId.ToString());

            if (success)
            {
                Plants = await _PlantService.GetAll();
            }
        }

        private async Task DeleteToDo(int toDoId)
        {
            var success = await _ToDoService.Delete(toDoId.ToString());

            if (success)
            {
                ToDos = await _ToDoService.GetAll();
            }
        }

        private bool IsOverdue(DateTime dueDate)
        {
            return dueDate < DateTime.Now;
        }
        
        private double GrowthProgress(Plant plant)
        {
            var daysSincePlanting = (DateTime.Now - plant.PlantingDate).TotalDays;
            var growthProgress = (daysSincePlanting / plant.GrowthDuration) * 100;
            
            growthProgress = Math.Min(100, Math.Max(0, growthProgress));

            return (int) Math.Round(growthProgress);
        }
    }
}