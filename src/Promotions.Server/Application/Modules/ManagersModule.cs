using Application.Services.Managers;
using Application.Services.Managers.Commands;
using Application.Services.Managers.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class ManagersModule : CarterModule
    {
        public ManagersModule() : base("/api/managers")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllManagersQuery());

                return Results.Ok(response);
            });

            app.MapGet("/{id}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetManagerByIdQuery(id));

                return Results.Ok(response);
            })
                .WithName("GetManagerById");

            app.MapPost("/", async (ManagerCreateOrEdit request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreateManagerCommand(request));

                return Results.CreatedAtRoute("GetManagerById", new { id = response.Id }, response);
            });


            app.MapPut("/{id}", async (Guid id, ManagerCreateOrEdit request, IMediator mediator) =>
            {
                await mediator.Send(new UpdateManagerCommand(id, request));

                return Results.NoContent();
            });

            app.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteManagerCommand(id));

                return Results.NoContent();
            });
        }
    }
}
