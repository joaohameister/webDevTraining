using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ToDo_API;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ToDoController(IConfiguration _config)
        {
            this._config = _config;
        }
        private static async Task<IEnumerable<TaskToDo>> SelectAllTasks(SqlConnection connection)
        {
            return await connection.QueryAsync<TaskToDo>("select * from toDoListdb");
        }
        // GET: all tasks
        [HttpGet]
        public async Task<ActionResult<List<TaskToDo>>> GetAllTasks()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<TaskToDo> taskList = await (SelectAllTasks(connection));
            return Ok(await(SelectAllTasks(connection)));
        }

        // GET api/<ToDo_Controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TaskToDo>>> GetTask(int taskId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var task = await connection.QueryFirstAsync<TaskToDo>("select * from toDoListdb where id=@Id",
                new { Id = taskId });

            return Ok(taskId);
        }

        // POST api/<ToDo_Controller>
        [HttpPost]
        public async Task<ActionResult<List<TaskToDo>>> CreateTask(TaskToDo task)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into toDoListdb(taskName) values(@taskName)", task);
            return Ok(await SelectAllTasks(connection));
        }

        // PUT api/<ToDo_Controller>/5
        [HttpPut]
        public async Task<ActionResult<List<TaskToDo>>> UpdateTask(TaskToDo task)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("update toDoListdb set taskName=@TaskName where id=@Id", task);

            return Ok(await SelectAllTasks(connection));
        }

        // DELETE api/<ToDo_Controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TaskToDo>>> DeleteTask(int taskId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from toDoListdb where id=@Id",
                new { Id = taskId });

            return Ok(await SelectAllTasks(connection));
        }
    }
}
