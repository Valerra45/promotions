using Application.Services.Partners;
using Application.Services.Partners.Commands;
using Application.Services.Partners.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class PartnersModule : CarterModule
    {
        public PartnersModule() : base("/api/partners")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllPartnersQuery());

                return Results.Ok(response);
            });

            app.MapGet("/{id}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetPartnerByIdQuery(id));

                return Results.Ok(response);
            })
                .WithName("GetPartnerById");

            app.MapPost("/", async (PartnerCreateOrEdit request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreatePartnerCommand(request));

                return Results.CreatedAtRoute("GetPartnerById", new { id = response.Id }, response);
            });

            app.MapPut("/{id}", async (Guid id, PartnerCreateOrEdit request, IMediator mediator) =>
            {
                await mediator.Send(new UpdatePartnerCommand(id, request));

                return Results.NoContent();
            });

            app.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeletePartnerCommand(id));

                return Results.NoContent();
            });
        }
    }
}
