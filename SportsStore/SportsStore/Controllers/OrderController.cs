using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers;

public class OrderController : Controller
{
    private readonly IOrderRepository repository;
    private readonly Cart cart;

    public OrderController(IOrderRepository repository, Cart cart)
    {
        this.repository = repository;
        this.cart = cart;
    }

    [Authorize]
    public ViewResult List()
    {
        return View(repository.Orders.Where(o => !o.Shipped));
    }

    [HttpPost]
    [Authorize]
    public IActionResult MarkShipped(int orderID)
    {
        Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
        if (order != null)
        {
            order.Shipped = true;
            repository.SaveOrder(order);
        }
        return RedirectToAction(nameof(List));
    }

    public ViewResult Checkout()
    {
        return View(new Order());
    }

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        if (!cart.Lines.Any())
        {
            ModelState.AddModelError("", "Sorry, your cart is empty!");
        }
        if (ModelState.IsValid)
        {
            order.Lines = cart.Lines.ToArray();
            repository.SaveOrder(order);
            return RedirectToAction(nameof(Completed));
        }
        else
        {
            return View(order);
        }
    }

    public ViewResult Completed()
    {
        cart.Clear();
        return View();
    }
}