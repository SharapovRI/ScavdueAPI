using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scavdue.Business.Interfaces;

namespace Scavdue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativeUnitController : ControllerBase
    {
        private readonly IAdministrativeUnitService _administrativeUnitService;

        public AdministrativeUnitController(IAdministrativeUnitService administrativeUnitService)
        {
            _administrativeUnitService = administrativeUnitService;
        }

        [HttpGet("/AdministrativeUnits/{unitName}")]
        public async Task<IActionResult> GetUnitsByName(string unitName)
        {
            var units = await _administrativeUnitService.GetUnitListByNameAsync(unitName);
            return Ok(units);
        }

        [HttpGet("/AdministrativeUnits/Country/{unitName}")]
        public async Task<IActionResult> GetCountryUnit(string unitName)
        {
            var units = await _administrativeUnitService.GetCountryAsync(unitName);
            return Ok(units);
        }

        [HttpGet("/AdministrativeUnits/{parentId:int}/ChildUnits")]
        public async Task<IActionResult> GetChildUnits(int parentId)
        {
            var units = await _administrativeUnitService.GetChildsAsync(parentId);
            return Ok(units);
        }
    }
}
