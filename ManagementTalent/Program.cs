using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using ManagementTalent.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionMySql = builder.Configuration.GetConnectionString("MysqlConn");

builder.Services.AddDbContext<MTDbContext>(opts =>
{
    opts.UseMySql(connectionMySql, ServerVersion.AutoDetect(connectionMySql));
});

builder.Services.AddScoped<IAssessmentRepositorySql, AssessmentRepositorySql>();
builder.Services.AddScoped<IAssessmentParamResultRepositorySql, AssessmentParamResultRepositorySql>();
builder.Services.AddScoped<IAssessmentResultRepositorySql, AssessmentResultRepositorySql>();
builder.Services.AddScoped<IColabRepositorySql, ColabRepositorySql>();
builder.Services.AddScoped<IGroupParameterRepositorySql, GroupParameterRepositorySql>();
builder.Services.AddScoped<IJobParameterBaseRepositorySql, JobParameterBaseRepositorySql>();
builder.Services.AddScoped<IJobRoleRepositorySql, JobRoleRepositorySql>();
builder.Services.AddScoped<ISeniorityRepositorySql, SeniorityRepositorySql>();
builder.Services.AddScoped<ISupervisorRepositorySql, SupervisorRepositorySql>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();