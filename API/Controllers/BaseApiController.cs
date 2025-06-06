﻿using API.RequestHelper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePageResult<T>(IGenericRepository<T> repository, 
        ISpecification<T> specification, int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repository.GetListWithSpecification(specification);
        if (!items.Any())
        {
            return NotFound("No items found");
        }
        var count = await repository.CountAsync(specification);
        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
        return Ok(pagination);
    }
}