using Api.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MiembrosController : ControllerBase
{
  private readonly IMemberService _memberService;

  public MiembrosController(IMemberService memberService)
  {
    _memberService = memberService;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Member>> GetMembers()
  {
    var members = _memberService.GetAll();
    return Ok(members);
  }

  [HttpGet("{id:int}")]
  public ActionResult<Member> GetMember(int id)
  {
    var member = _memberService.GetById(id);
    return member is null ? NotFound() : Ok(member);
  }

  [HttpPost]
  public ActionResult<Member> CreateMember(Member member)
  {
    var created = _memberService.Create(member);
    return CreatedAtAction(nameof(GetMember), new { id = created.Id }, created);
  }

  [HttpPut("{id:int}")]
  public IActionResult UpdateMember(int id, Member member)
  {
    var updated = _memberService.Update(id, member);
    return updated ? NoContent() : NotFound();
  }

  [HttpDelete("{id:int}")]
  public IActionResult DeleteMember(int id)
  {
    var deleted = _memberService.Delete(id);
    return deleted ? NoContent() : NotFound();
  }
}
