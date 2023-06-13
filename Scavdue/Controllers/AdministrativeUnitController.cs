﻿using AutoMapper;
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
        private readonly IUnitObjectsService _unitObjectsService;

        public AdministrativeUnitController(IAdministrativeUnitService administrativeUnitService, IUnitObjectsService unitObjectsService)
        {
            _administrativeUnitService = administrativeUnitService;
            _unitObjectsService = unitObjectsService;
        }

        [HttpGet("/AdministrativeUnits/{unitName}")]
        public async Task<IActionResult> GetUnitsByName(string unitName)
        {
            var units = await _administrativeUnitService.GetUnitListByNameAsync(unitName);
            return Ok(units);
        }

        [HttpGet("/AdministrativeUnits/Country/{countryId}/CityList")]
        public async Task<IActionResult> GetCityList(int countryId)
        {
            var units = await _administrativeUnitService.GetCitiesList(countryId);
            return Ok(units);
        }

        [HttpGet("/AdministrativeUnits/{unitId:int}/LifeIndex")]
        public async Task<IActionResult> GetUnitLifeIndex(int unitId)
        {
            var unit = await _administrativeUnitService.GetUnitLifeIndex(unitId);
            return Ok(unit);
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

        [HttpGet("/AdministrativeUnits/{unitId:int}/UnitObjects")]
        public async Task<IActionResult> GetUnitObjects(int unitId)
        {
            var units = await _unitObjectsService.GetUnitObjects(unitId);
            return Ok(units);
        }

        [HttpGet("/AdministrativeUnits/Moderator/ComplexUpdate/{countryName}")]
        public async Task<IActionResult> GetComplexUpdate(string countryName)
        {
            var message = await _administrativeUnitService.AdminComplexAdminUnits(countryName);
            return Ok(message);
        }

        [HttpGet("/AdministrativeUnits/Moderator/UnitUpdate/{unitId:int}")]
        public async Task<IActionResult> GetUnitUpdate(int unitId)
        {
            //var message = await _administrativeUnitService.AdminComplexAdminUnits(unitName);
            return Ok();
        }

        /*[HttpGet("/AdministrativeUnits/Admin/TestUnits/")]
        public async Task<IActionResult> GetComplexTestUnits()
        {
            var message = await _administrativeUnitService.TestUnits();
            return Ok(message);
        }*/
    }
}
