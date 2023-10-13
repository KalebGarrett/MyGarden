using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyGarden.Models;
using MyGarden.Web.Services;

namespace MyGarden.Web.Pages
{
    public partial class AddToDo
    {
        private ToDo ToDo { get; set; } = new ToDo();
        private bool Added { get; set; }
        [Inject]
        private ToDoService _ToDoService { get; set; }
        
        private async Task AddToDoToDb()
        {
            var addToDo = await _ToDoService.Create(ToDo);

            if (addToDo != null)
            {
                Added = true;
                ToDo = new ToDo();
            }
        }
    }
}