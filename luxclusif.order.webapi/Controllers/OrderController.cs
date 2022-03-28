using luxclusif.order.application.Models;
using luxclusif.order.application.UseCases.Order.CreateOrder;
using luxclusif.order.webapi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace luxclusif.order.webapi.Controllers
{
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class OrderController : BaseController
    {
        private readonly IMediator mediator;
        public OrderController(IMediator mediator,
            Notifier notifier) : base(notifier)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        [SwaggerOperation(
            OperationId = "Post_New_User",
            Summary = "Post new user")]
        [SwaggerResponse(200, Type = typeof(DefaultResponseDto<CreateOrderInput>), Description = "Create new user")]
        [SwaggerResponse(400, Type = typeof(DefaultResponseDto<object>), Description = "Error")]
        [SwaggerResponse(500, Type = typeof(DefaultResponseDto<object>), Description = "Error")]
        public async Task<IActionResult> CreateConfiguration([FromBody] CreateOrderInput model)
        {
            var ret = await mediator.Send<CreateOrderOutput>(model);

            return Result(ret);
        }
    }
}
