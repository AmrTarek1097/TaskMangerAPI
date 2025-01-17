namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.Tasks
            .Include(m => m.TeamMember)
            .ToListAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks
            .Include(m => m.TeamMember)
           .SingleOrDefaultAsync(m => m.MemberTaskId == id);

            if (task is null)
                return BadRequest($"No task with this ID! {id}");

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskDto dto)
        {
            if (dto.Name == null)
                return BadRequest("Name is Required!");

            if (dto.Description == null)
                return BadRequest("Description is Required!");

            if (dto.TeamMemberId == null || dto.TeamMemberId <= 0)
                return BadRequest("Team member not exist!");

            var task = new MemberTask
            {
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TeamMemberId = dto.TeamMemberId,

            };
            await _context.AddAsync(task);

            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskById(int id, [FromBody] TaskDto dto)
        {
            var task = await _context.Tasks
            .SingleOrDefaultAsync(m => m.MemberTaskId == id);


            if (task is null)
                return BadRequest($"No task with this ID! {id}");


            task.Name = dto.Name;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.StartDate = dto.StartDate;
            task.EndDate = dto.EndDate;
            task.TeamMemberId = dto.TeamMemberId;


            _context.Update(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskById(int id)
        {
            var task = await _context.Tasks
            .SingleOrDefaultAsync(m => m.MemberTaskId == id);


            if (task is null)
                return BadRequest($"No task with this ID! {id}");

            _context.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }
    }
}
