var builder = WebApplication.CreateBuilder(args);

//inyectaremos las dependencias

var app = builder.Build();

//configurar el HTTP

app.Run();
