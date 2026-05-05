using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;
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
    public async Task<ActionResult<GetByIdResponse>> GetById(Guid Id)
    {
        return Ok(await _infouservice.GetById(Id));
    }

    [HttpPatch("{Id}")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid Id, [FromBody] UpdateRequest infouserDto)
    {
        return Ok(await _infouservice.Update(Id, infouserDto));
    }
    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest infouserDto)
    {
        return await _infouservice.Create(infouserDto);
    }

    [HttpDelete("{Id}")]

    public async Task<ActionResult> Delete(Guid Id)
    {
        await _infouservice.Delete(Id);

        return NoContent();
    }
}