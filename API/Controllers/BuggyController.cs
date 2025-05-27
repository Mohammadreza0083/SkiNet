using API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized("You are not authorized to access this resource.");
    }
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("This is a bad request.");
    }
    [HttpGet("notfound")]
    public ActionResult GetNotFound()
    {
        return NotFound("The requested resource was not found.");
    }
    [HttpGet("internalerror")]
    public ActionResult GetInternalError()
    {
        throw new Exception("This is an internal server error.");
    }
    [HttpPost("validationerror")]
    public ActionResult GetValidationError(CreateProductDto product)
    {
        return Ok();
    }
}