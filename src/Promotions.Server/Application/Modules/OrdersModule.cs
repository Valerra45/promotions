using Application.Services.Orders;
using Application.Services.Orders.Commands;
using Application.Services.Orders.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class OrdersModule : CarterModule
    {
        public OrdersModule() : base("/api/orders")
        {

        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllOrdersQuery());

                return Results.Ok(response);
            });

            app.MapGet("/{id}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetOrderByIdQuery(id));

                return Results.Ok(response);
            })
                .WithName("GetOrderById");

            app.MapGet("/time", async ([FromBody] OrdersByTimeRequest time, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetOrdersByTimeQuery(time));

                return Results.Ok(response);
            });

            app.MapGet("/partner", async ([FromBody] ByPartnerRequest partner, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetOrdersByPartnerQuery(partner));

                return Results.Ok(response);
            });

            app.MapPost("/", async (OrderCreateOrEdit request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreateOrderCommand(request));

                return Results.CreatedAtRoute("GetOrderById", new { id = response.Id }, response);
            });

            app.MapPut("/{id}", async (Guid id, OrderCreateOrEdit request, IMediator mediator) =>
            {
                await mediator.Send(new UpdateOrderCommand(id, request));

                return Results.NoContent();
            });

            app.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteOrderCommand(id));

                return Results.NoContent();
            });
        }
    }
}
