using OrderProject.SoapService.ServiceContract;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseSoapEndpoint<ICustomerService>("/CustomerService.asmx", new SoapEncoderOptions());
app.Run();
