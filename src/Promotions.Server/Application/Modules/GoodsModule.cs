using Application.Services.GoodsService;
using Application.Services.GoodsService.Commands;
using Application.Services.GoodsService.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class GoodsModule : CarterModule
    {
        public GoodsModule() : base("/api/goods")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllGoodsQuery());

                return Results.Ok(response);
            });

            app.MapGet("/{id}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetGoodsByIdQuery(id));

                return Results.Ok(response);
            })
                .WithName("GetGoodsById");

            app.MapPost("/", async (GoodsCreateOrEdit request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreateGoodsCommand(request));

                return Results.CreatedAtRoute("GetGoodsById", new { id = response.Id }, response);
            });

            app.MapPut("/{id}", async (Guid id, GoodsCreateOrEdit request, IMediator mediator) =>
            {
                await mediator.Send(new UpdateGoodsCommand(id, request));

                return Results.NoContent();
            });

            app.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteGoodsCommand(id));

                return Results.NoContent();
            });
        }
    }
}
