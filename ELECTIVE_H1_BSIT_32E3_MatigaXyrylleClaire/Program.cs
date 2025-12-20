using ELECTIVE_H1_BSIT_32E3_MatigaXyrylleClaire.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ IMPORTANT: Register ResolutionService as Singleton (in-memory storage)
builder.Services.AddSingleton<ResolutionService>();

// ✅ IMPORTANT: Add CORS policy for the HTML UI to work
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ IMPORTANT: Enable CORS before authorization
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();