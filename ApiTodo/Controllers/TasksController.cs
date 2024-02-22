using ApiTodo.Repos;
using ApiTodo.Repos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiTodo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(DataContext context) : ControllerBase
    {
        private readonly DataContext contextdata = context;

        [HttpGet("completed")]
        public async Task<ActionResult> GetCompletedTasks()
        {
            string sqlquery = "exec sp_GetTaskByStatus @status_task = 'completado'";
            var data = await this.contextdata.Tasks.FromSqlRaw(sqlquery).ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("progress")]
        public async Task<ActionResult> GetInProgressTasks()
        {
            string sqlquery = "exec sp_GetTaskByStatus @status_task = 'en progreso'";
            var data = await this.contextdata.Tasks.FromSqlRaw(sqlquery).ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("postponed")]
        public async Task<ActionResult> GetPostponedTasks()
        {
            string sqlquery = "exec sp_GetTaskByStatus @status_task = 'postergado'";
            var data = await this.contextdata.Tasks.FromSqlRaw(sqlquery).ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            string sqlQuery = "exec sp_GetTaskById @id";
            SqlParameter parameter = new ("@id", id);

            var data = await this.contextdata.Tasks.FromSqlRaw(sqlQuery, parameter).ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Repos.Models.Task task)
        {
            string sqlQuery = "exec sp_AddTask @title, @autor, @status_task, @description, @start_date, @end_date";
            SqlParameter[] parameters =
            [
                 new("@title", task.Title),
                 new("@autor", task.Autor),
                 new("@status_task", task.StatusTask),
                 new("@description", task.Description),
                 new("@start_date", task.StartDate),
                 new("@end_date", task.EndDate),
             ];

            var data = await this.contextdata.Database.ExecuteSqlRawAsync(sqlQuery, parameters);
            
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, Repos.Models.Task task)
        {
            string sqlQuery = "exec sp_UpdateTask @id, @title, @autor, @status_task, @description, @start_date, @end_date";
            SqlParameter[] parameters =
            [
                new("@id", id),
                new("@title", task.Title),
                new("@autor", task.Autor),
                new("@status_task", task.StatusTask),
                new("@description", task.Description),
                new("@start_date", task.StartDate),
                new("@end_date", task.EndDate),
            ];

            var data = await this.contextdata.Database.ExecuteSqlRawAsync(sqlQuery, parameters);

            return Ok(data);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTask(int id, Repos.Models.Task task)
        {
            string sqlQuery = "exec sp_UpdateTaskStatus @id, @status_task";
            SqlParameter[] parameters =
            [
                new("@id", id),
                new("@status_task", task.StatusTask),
            ];

            var data = await this.contextdata.Database.ExecuteSqlRawAsync(sqlQuery, parameters);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            string sqlQuery = "exec sp_DeleteTask @id";
            SqlParameter[] parameter =
            [
                new("@id", id)
            ];
            var data = await this.contextdata.Database.ExecuteSqlRawAsync(sqlQuery, parameter);
            
            return Ok(data);
        }

    }

}