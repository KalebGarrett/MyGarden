using Microsoft.AspNetCore.Components;
using MyGarden.Models;
using MyGarden.Web.Services;

namespace MyGarden.Web.Pages
{
    public partial class UpdateToDo: ComponentBase
    {
        [Inject]
        public ToDoService _ToDoService { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Parameter]
        public string Id { get; set; }

        public ToDo ToDo { get; set; } = new ToDo();

        private bool Updated { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            ToDo = await _ToDoService.Get(Id);
            if (ToDo == null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        public async Task UpdateToDoDb()
        {
            ToDo = await _ToDoService.Update(Id, ToDo);

            if (ToDo != null)
            {
                Updated = true;
            }
        }
    }
}