var builder = WebApplication.CreateBuilder(args);

//inyectaremos las dependencias osea la dependencias
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    //Setup oficial 
    //Va leer todos los handlers automaticamente
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
var app = builder.Build();

//configurar el HTTP
//le damos accesso a Carter con sus modulos en las rutas
app.MapCarter();
app.Run();
