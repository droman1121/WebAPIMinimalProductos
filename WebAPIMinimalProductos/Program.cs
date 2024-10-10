using Microsoft.EntityFrameworkCore;
using WebAPIMinimalProductos.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProductoConexion") ?? "DataSource=Producto.db";
builder.Services.AddDbContext<ProductContext>(opt => opt.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/productos", async (ProductContext db) =>
    await db.Productos.ToListAsync());

//app.MapGet("/productos/complete", async (ProductContext db) =>
//    await db.Productos.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/productos/{id}", async (int id, ProductContext db) =>
    await db.Productos.FindAsync(id)
        is Producto todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/productos", async (Producto todo, ProductContext db) =>
{
    db.Productos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/productos/{todo.ID}", todo);
});

app.MapPut("/productos/{id}", async (int id, Producto inputTodo, ProductContext db) =>
{
    var todo = await db.Productos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.ID = inputTodo.ID;
    todo.Nombre = inputTodo.Nombre;
    todo.Descripcion = inputTodo.Descripcion;
    todo.Precio = inputTodo.Precio;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/productos/{id}", async (int id, ProductContext db) =>
{
    if (await db.Productos.FindAsync(id) is Producto todo)
    {
        db.Productos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();