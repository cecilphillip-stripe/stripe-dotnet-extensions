using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SampleCheckout.Models;
using Stripe;

namespace SampleCheckout.Controllers;

public class HomeController : Controller
{
    private readonly IStripeClient _stripeClient;
    private readonly ProductService _productService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IStripeClient stripeClient, ProductService productService, ILogger<HomeController> logger)
    {
        _stripeClient = stripeClient;
        _productService = productService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}