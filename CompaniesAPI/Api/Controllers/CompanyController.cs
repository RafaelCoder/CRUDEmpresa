﻿using CompaniesAPI.Api.Contracts;
using CompaniesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace CompaniesAPI.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    [Route("{id}", Name = "Get")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var company = await _companyService.GetAsync(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCompany([FromBody] CompanyCreateContract contract)
    {
        try
        {
            var newCompany = await _companyService.AddAsync(contract);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
