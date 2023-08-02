using KolibSoft.AuthStore.Core;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.AuthStore.Server.Example;

[Route("[controller]")]
public class TestController : ControllerBase
{

    [HttpGet]
    public IActionResult Test([FromServices] AuthStoreContext context)
    {
        return Ok();
    }

}