using API.Errors;
using API.Extensions;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

await builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value!.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse()
        {
            Errors = errors
        };
        return new BadRequestObjectResult(errorResponse);
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

// app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
