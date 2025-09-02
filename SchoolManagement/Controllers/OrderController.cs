using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Orders;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrders _svc;
    public OrderController(IOrders svc) => _svc = svc;

    [HttpGet]
    public ActionResult<List<OrderResponseVM>> GetAll() => _svc.GetAllOrders();

    [HttpGet("{id:int}")]
    public ActionResult<OrderResponseVM> GetById(int id)
    {
        var vm = _svc.GetOrderById(id);
        return vm is null ? NotFound() : Ok(vm);
    }

    [HttpGet("supplier/{supplierId:int}")]
    public ActionResult<List<OrderResponseVM>> GetBySupplier(int supplierId)
        => _svc.GetBySupplier(supplierId);

    [HttpPost]
    public ActionResult<OrderResponseVM> Create([FromBody] OrderCreateVM vm)
    {
        var created = _svc.CreateOrder(vm);
        return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
    }

    [HttpPut("{id:int}")]
    public ActionResult<OrderResponseVM> Update(int id, [FromBody] OrderRequestVM vm)
    {
        var updated = _svc.UpdateOrder(id, vm);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id, [FromQuery] int userId)
        => _svc.DeleteOrder(id, userId) ? NoContent() : NotFound();
}
