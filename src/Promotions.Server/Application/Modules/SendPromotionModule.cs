using Application.Services.Orders;
using Application.Services.SendPromotions;
using Application.Services.SendPromotions.Commands;
using Application.Services.SendPromotions.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class SendPromotionModule : CarterModule
    {
        public SendPromotionModule() : base("/api/sendPromotion")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/partner", async ([FromBody] ByPartnerRequest request, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetSendPromotionByPartner(request));

                return Results.Ok(response);
            });

            app.MapGet("/{id}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetSendPromotionById(id));

                return Results.Ok(response);
            })
                .WithName("GetSendPromotionById");

            app.MapPost("/", async (SendPromotionCreateOrEdit request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreateSendPromotionCommand(request));

                return Results.CreatedAtRoute("GetSendPromotionById", new { id = response.Id }, response);
            });

            app.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteSendPromotionCommand(id));

                return Results.NoContent();
            });
        }
    }
}
