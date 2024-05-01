var builder = WebApplication.CreateBuilder(args);

// Add API controllers to the builder service
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS (Cross Origin Resource Sharing) Service
builder.Services.AddCors(
    (options) =>
    {
        options.AddPolicy("DevCors", (corsBuilder) =>
        {
            corsBuilder.WithOrigins("http://locahost:4200", "http://locahost:3000", "http://locahost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
        options.AddPolicy("ProdCors", (corsBuilder) =>
       {
           corsBuilder.WithOrigins("http://productionsite.com")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
       });

    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //define the cors here
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{ 
    //define the cors here
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}

app.MapControllers();



// app.MapGet("/weatherforecast", () =>
// {

// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.Run();


