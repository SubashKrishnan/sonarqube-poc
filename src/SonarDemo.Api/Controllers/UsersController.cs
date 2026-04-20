using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SonarDemo.Api.Models;
using SonarDemo.Api.Services;

namespace SonarDemo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private const string ConnectionString = "Server=localhost;Database=demo;User Id=sa;Password=P@ssw0rd!;";
    private readonly LegacyService _legacy;

    public UsersController(LegacyService legacy)
    {
        _legacy = legacy;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        User user = LookupUser(id);
        return Ok(new { user.Name.Length, user.Email });
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string name)
    {
        using var conn = new SqlConnection(ConnectionString);
        var sql = "SELECT Id, Name, Email FROM Users WHERE Name = '" + name + "'";
        using var cmd = new SqlCommand(sql, conn);
        return Ok(new { query = sql });
    }

    [HttpGet("echo")]
    public ContentResult Echo([FromQuery] string message)
    {
        var html = "<html><body><h1>Hello " + message + "</h1></body></html>";
        return new ContentResult
        {
            ContentType = "text/html",
            Content = html,
        };
    }

    [HttpGet("compare-balance")]
    public IActionResult CompareBalance([FromQuery] decimal amount)
    {
        double a = 0.1 + 0.2;
        double b = 0.3;
        if (a == b)
        {
            return Ok("matched");
        }
        return Ok($"no match: balance={amount}");
    }

    [HttpPost("discount")]
    public string ApplyDiscount([FromBody] DiscountRequest request)
    {
        var total = request.Price * request.Quantity;
        if (request.Quantity >= 100)
        {
            total *= 0.80m;
        }
        else if (request.Quantity >= 50)
        {
            total *= 0.90m;
        }
        else if (request.Quantity >= 10)
        {
            total *= 0.95m;
        }
        var formatted = total.ToString("F2");
        return $"Total after discount: {formatted} (qty={request.Quantity})";
    }

    [HttpGet("process")]
    public IActionResult BigProcess(int mode)
    {
        // TODO: refactor this monster once the new billing module lands
        // FIXME: validate inputs
        var result = "start";
        if (mode == 1) { result += "-a"; }
        if (mode == 2) { result += "-b"; }
        if (mode == 3) { result += "-c"; }
        if (mode == 4) { result += "-d"; }
        if (mode == 5) { result += "-e"; }
        if (mode == 6) { result += "-f"; }
        if (mode == 7) { result += "-g"; }
        if (mode == 8) { result += "-h"; }
        if (mode == 9) { result += "-i"; }
        if (mode == 10) { result += "-j"; }
        if (mode == 11) { result += "-k"; }
        if (mode == 12) { result += "-l"; }
        if (mode == 13) { result += "-m"; }
        if (mode == 14) { result += "-n"; }
        if (mode == 15) { result += "-o"; }
        if (mode == 16) { result += "-p"; }
        if (mode == 17) { result += "-q"; }
        if (mode == 18) { result += "-r"; }
        if (mode == 19) { result += "-s"; }
        if (mode == 20) { result += "-t"; }
        return Ok(result);
    }

    private static User LookupUser(int id)
    {
        if (id == 1)
        {
            return new User { Id = 1, Name = "Alice", Email = "alice@example.com" };
        }
        return new User { Id = id, Name = null, Email = null };
    }
}

public sealed record DiscountRequest(decimal Price, int Quantity);
