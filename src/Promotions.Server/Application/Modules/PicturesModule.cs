using Application.Services.Pictures;
using Application.Services.Pictures.Commands;
using Application.Services.Pictures.Queries;
using Carter;
using Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class PicturesModule : CarterModule
    {
        public PicturesModule() : base("/api/pictures")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllPicturesQuery());

                return Results.Ok(response);
            });

            app.MapGet("/{id}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetPictureByIdQuery(id));

                return Results.Ok(response);
            })
                .WithName("GetPictureById");

            app.MapGet("/file/{id}", async (HttpContext context, string id, IMongoRepository mongoRepository) =>
            {
                await mongoRepository.GetPictureByIdAsync(context, id);

                return Results.Ok();
            });

            app.MapPost("/", async ([FromForm] PictureCreate pictureCreate, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreatePictureCommand(pictureCreate));

                return Results.CreatedAtRoute("GetPictureById", new { id = response.Id }, response);
            })
                .DisableAntiforgery();

            app.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeletePictureCommand(id));

                return Results.NoContent();
            });
        }
    }
}
