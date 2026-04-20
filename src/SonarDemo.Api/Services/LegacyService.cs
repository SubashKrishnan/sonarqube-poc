using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;

namespace SonarDemo.Api.Services;

public sealed class LegacyService
{
    public string HashPassword(string password)
    {
        using var md5 = MD5.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = md5.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }

    public HttpClient CreateInsecureClient()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
        };
        return new HttpClient(handler);
    }

    public string ClassifyOrder(int quantity, decimal total, string country, bool isVip, string paymentMethod)
    {
        string tier;
        if (quantity > 100)
        {
            if (total > 10000)
            {
                if (isVip)
                {
                    if (country == "US" || country == "UK")
                    {
                        tier = paymentMethod == "WIRE" ? "PLATINUM-EXPEDITED" : "PLATINUM";
                    }
                    else if (country == "DE")
                    {
                        tier = "GOLD-EU";
                    }
                    else
                    {
                        tier = "GOLD";
                    }
                }
                else
                {
                    tier = total > 50000 ? "GOLD-NEWVIP" : "SILVER";
                }
            }
            else if (total > 5000)
            {
                tier = isVip ? "SILVER-VIP" : "BRONZE";
            }
            else
            {
                tier = "BRONZE";
            }
        }
        else if (quantity > 10)
        {
            if (total > 1000)
            {
                tier = isVip ? "BRONZE" : "BASIC";
            }
            else
            {
                tier = "BASIC";
            }
        }
        else
        {
            tier = paymentMethod == "CASH" ? "CASH-ONLY" : "BASIC";
        }
        return tier;
    }

    public async void FireAndForgetAudit(string action)
    {
        await Task.Delay(10);
        Console.WriteLine($"[audit] {action}");
    }

    private string UnusedHelper(string input)
    {
        return input.ToUpperInvariant().Trim();
    }

    public string ApplyDiscountV1(decimal price, int quantity)
    {
        var total = price * quantity;
        if (quantity >= 100)
        {
            total *= 0.80m;
        }
        else if (quantity >= 50)
        {
            total *= 0.90m;
        }
        else if (quantity >= 10)
        {
            total *= 0.95m;
        }
        var formatted = total.ToString("F2");
        return $"Total after discount: {formatted} (qty={quantity})";
    }
}
