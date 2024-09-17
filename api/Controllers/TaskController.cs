using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TaskController : ControllerBase {

        public TaskController(){

        }

        // /api/task
        [HttpGet]
        public String GetTasks()
        {
            return "";
        }

        // /api/task/1000
        [HttpGet("{id}")]
        public String GetTask(int id)
        {
            return "";
        }

        
        // /api/task
        [HttpPost]
        public String CreateTask()
        {
            return "";
        }

        
        // /api/task
        [HttpPut("{id}")]
        public String UpdateTask(int id, String task)
        {
            return "";
        }
        
        // /api/task
        [HttpDelete("{id}")]
        public String DeleteTask(int id)
        {
            return "";
        }
    
    }
}