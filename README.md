
## Add-a-Task Feature

### Overview

The "Add-a-Task" feature has been implemented using .NET Core to enhance the project management platform. This feature allows users to create new tasks with essential details and integrates seamlessly with the existing system.

### Key Features

- **Task Creation**: Users can add new tasks by providing essential details such as title, description, and due date.
- **Validation**: Input validation is implemented to ensure that task data is complete and accurate.
- **Integration**: The feature integrates with the existing backend API to store task information in the database.

### Implementation Details

- **Backend**: Developed using .NET Core.
  - **API Endpoint**: `POST /api/tasks`
  - **Model**: `Task`
    ```csharp
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
    ```
  - **Controller**: `TasksController`
    - **Action**: `CreateTask`
      ```csharp
      [HttpPost]
      public IActionResult CreateTask([FromBody] Task task)
      {
          if (ModelState.IsValid)
          {
              _taskService.AddTask(task);
              return Ok();
          }
          return BadRequest(ModelState);
      }
      ```

- **Frontend**: Integrated with the user interface to provide a form for task creation.
  - **Form Fields**: Title, Description, Due Date
  - **Validation**: Basic client-side validation is included to ensure required fields are filled out.

### How to Use

1. **Add Task**:
   - Navigate to the task creation page.
   - Fill in the task details (Title, Description, Due Date).
   - Submit the form to create a new task.

2. **API Documentation**:
   - The API endpoint for creating tasks is `/api/tasks`.
   - It accepts POST requests with a JSON payload containing task details.

### Contribution

- **Developer**: Maqbul Pasha
- **Date**: 11th-Sept-2024

For any questions or feedback regarding this feature, please reach out to Maqbul Pasha or the project team.
