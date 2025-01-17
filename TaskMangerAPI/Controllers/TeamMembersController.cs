using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeamMember()
        {
            var members = await _context.TeamMembers
           .OrderByDescending(m => m.Name)
           .ToListAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _context.TeamMembers
           .SingleOrDefaultAsync(m => m.TeamMemberId == id);

            if (member is null)
                return BadRequest($"No member with this ID! {id}");

            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeamMember(TeamMemberDto dto)
        {
            if (dto.Name == null)
                return BadRequest("Name is Required!");

            if (dto.Email == null)
                return BadRequest("Email is Required!");

            var member = new TeamMember
            {
                Name = dto.Name,
                Email = dto.Email,

            };
            await _context.AddAsync(member);
            await _context.SaveChangesAsync();
            return Ok(dto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMemberById(int id, [FromBody] TeamMemberDto dto)
        {
            var member = await _context.TeamMembers
            .SingleOrDefaultAsync(m => m.TeamMemberId == id);


            if (member is null)
                return BadRequest($"No member with this ID! {id}");



            member.Name = dto.Name;
                member.Email = dto.Email;

            
            _context.Update(member);
            await _context.SaveChangesAsync();

            return Ok(member);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemberById(int id)
        {
            var member = await _context.TeamMembers
            .SingleOrDefaultAsync(m => m.TeamMemberId == id);


            if (member is null)
                return BadRequest($"No member with this ID! {id}");

            _context.Remove(member);
            await _context.SaveChangesAsync();

            return Ok(member);
        }
    }
}
