using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scavdue.Business.Interfaces;
using Scavdue.Business.Services;

namespace Scavdue.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnitObjectController : ControllerBase
{
    private readonly IUnitObjectsService _unitObjectsService;

    public UnitObjectController(IUnitObjectsService unitObjectsService)
    {
        _unitObjectsService = unitObjectsService;
    }

    [HttpGet("/AdministrativeUnits/{unitId:int}/ObjectsCount")]
    public async Task<IActionResult> GetChildUnits(int unitId)
    {
        var units = await _unitObjectsService.GetBuildingClassCount(unitId);
        return Ok(units);
    }
}
