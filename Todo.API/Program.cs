using Todo.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = "Todo.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory , xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureAutoMapperService();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureCORS();

var app = builder.Build();

app.ConfigureExceptionHandler();
app.UseCors("AllowCORS");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
