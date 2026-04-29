using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/infoUser")]
[ApiController]

public class InfoUserController : ControllerBase
{
    private readonly IInfoUserService _infouservice;

    public InfoUserController(IInfoUserService infouserservice)
    {
        _infouservice = infouserservice;
    }
    [HttpGet("{Id}")]
    public async Task<ActionResult<InfoUser>> GetById(Guid Id)
    {
        return Ok(await _infouservice.GetById(Id));
    }

    [HttpPatch]
    public async Task<ActionResult<InfoUser>> Update([FromBody] InfoUser infouser)
    {
        await _infouservice.Update(infouser);

        return Ok();
    }
    [HttpPost]
    public ActionResult Create([FromBody] InfoUser infouser)
    {
        _infouservice.Create(infouser);

        return CreatedAtAction(nameof(infouser), new { id = infouser.Id }, infouser);
    }

    [HttpDelete("{Id}")]

    public async Task<ActionResult> Delete(Guid Id)
    {
        await _infouservice.Delete(Id);

        return Ok();
    }
}