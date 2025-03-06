using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddSwaggerGen();

    services.AddControllers();

    services.Configure<GamesDatabaseSettings>(builder.Configuration.GetSection(nameof(GamesDatabaseSettings)));

    services.AddSingleton<IGamesDatabaseSettings>(sp => sp.GetRequiredService<IOptions<GamesDatabaseSettings>>().Value);

    services.AddSingleton<GamesService>();
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.MapControllers();
}

app.Run();