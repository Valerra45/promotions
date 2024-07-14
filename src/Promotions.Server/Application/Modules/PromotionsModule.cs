using Application.Services.Promotions;
using Application.Services.Promotions.Commands;
using Application.Services.Promotions.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class PromotionsModule : CarterModule
    {
        public PromotionsModule() : base("/api/promotions")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("", async ([FromBody] List<Guid> ids, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetRangeByIdsPromotionsQuery(ids));

                return Results.Ok(response);
            });

            app.MapGet("/{id}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetPromotionByIdQuery(id));

                return Results.Ok(response);
            })
                .WithName("GetPromotionById");

            app.MapPost("/", async (PromotionCreateOrEdit request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreatePromotionCommand(request));

                return Results.CreatedAtRoute("GetPromotionById", new { id = response.Id }, response);
            });

            app.MapPut("/{id}", async (Guid id, PromotionCreateOrEdit request, IMediator mediator) =>
            {
                await mediator.Send(new UpdatePromotionCommand(id, request));

                return Results.NoContent();
            });

            app.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeletePromotionCommand(id));

                return Results.NoContent();
            });
        }
    }
}
