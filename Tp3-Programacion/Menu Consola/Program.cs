using Tp3_Programacion.Data;
using Tp3_Programacion.Models;
using Tp3_Programacion.Repository;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        using var context = new ApplicationDbcontext();
        var repoProducots = new RepositorioBase<Producto>(context);
        var repoClientes = new RepositorioBase<Cliente>(context);


        while (true)
        {
            Console.WriteLine("1- Registrar Producto");
            Console.WriteLine("2- Registrar Cliente");
            Console.WriteLine("3- Registrar Venta");
            Console.WriteLine("4- Reporte de ventas por Cliente");
            Console.WriteLine("5- Salir");
            var opcion = Console.ReadLine();



            switch (opcion)
            {
                case "1": RegistrarProducto (repoProducots); break;
                case "2": RegistrarCliente (repoClientes); break;
                case "3": RegistrarVenta (context); break;
                case "4": ReporteVentasPorCliente(context); break;
                case "5": return;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }


    static void RegistrarProducto(IRepositorio<Producto> repo)
    {
        Console.WriteLine("Ingrese el nombre del producto:");
        var nombre = Console.ReadLine();
        Console.WriteLine("Ingrese el precio del producto:");
        var precio = decimal.Parse(Console.ReadLine());
        var producto = new Producto { Nombre = nombre, Precio = precio };
        repo.Agregar(producto);
        repo.Guardar();
        Console.WriteLine("Producto registrado.");
    }



    static void RegistrarCliente (IRepositorio<Cliente> repo)
    {
        Console.WriteLine("Nombre::");
        var nombre = Console.ReadLine();
        Console.WriteLine("Email:");
        var email = Console.ReadLine();
        var cliente = new Cliente { Nombre = nombre, Email = email };
        repo.Agregar(cliente);
        repo.Guardar();
        Console.WriteLine("Cliente registrado.");
    }



    static void RegistrarVenta(ApplicationDbcontext context)
    {
        Console.WriteLine("Nombre del Cliente:");
        var nombreCliente = Console.ReadLine();
        var cliente = context.Clientes.FirstOrDefault(c => c.Nombre == nombreCliente);
        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        var venta = new Venta { Fecha = DateTime.Now, ClienteId = cliente.Id };
        context.Ventas.Add(venta);
        context.SaveChanges();



        while (true)
        {
            Console.WriteLine("Productos disponibles:");
            foreach (var p in context.Productos)
            {
                Console.WriteLine($"- {p.Nombre} (${p.Precio})");
            }

            Console.WriteLine("Nombre del Producto (ENTER para terminar): ");
            var nombreProducto = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombreProducto)) break;

            var producto = context.Productos
                .FirstOrDefault(p => p.Nombre.ToLower() == nombreProducto.ToLower().Trim());

            if (producto == null)
            {
                Console.WriteLine("Producto no encontado.");
                continue;
            }

            Console.WriteLine("Cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad invalida.");
                continue;
            }

            var ventaDetalle = new VentaDetalle
            {
                VentaId = venta.Id,
                ProductoId = producto.Id,
                Cantidad = cantidad
            };
            context.VentaDetalles.Add(ventaDetalle);
            context.SaveChanges();
        }

        Console.WriteLine("Venta registrada.");
    }


    static void ReporteVentasPorCliente(ApplicationDbcontext context)
    {
        var clientes = context.Clientes
            .Include(c => c.Ventas)
            .ThenInclude(v => v.Detalles)
            .ThenInclude(d => d.Producto)
            .ToList();

        foreach (var cliente in clientes)
        {
            Console.WriteLine($"\nCliente: {cliente.Nombre}");
            foreach (var venta in cliente.Ventas)
            {
                Console.WriteLine($"  Venta {venta.Id} - {venta.Fecha}");
                foreach (var detalle in venta.Detalles)
                {
                    var total = detalle.Producto.Precio * detalle.Cantidad;
                    Console.WriteLine($"    {detalle.Producto.Nombre} x{detalle.Cantidad} - ${total}");
                }
            }
        }
    }
}




