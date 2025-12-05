
using Tp3_Programacion.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tp3_Programacion.Data;
using Tp3_Programacion.Models;


class Program
{
    static void Main(string[] args)
    {
        using var db = new AplicationDbContextcs();

        bool salir = false;
        while (!salir)
        {

            Console.WriteLine("1. Registrar Cancha");
            Console.WriteLine("2. Registrar Socio");
            Console.WriteLine("3. Registrar Reserva");
            Console.WriteLine("4. Listar Reservas Vigentes");
            Console.WriteLine("5. Reporte de Uso por Cancha");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": RegistrarCancha(db); break;
                case "2": RegistrarSocio(db); break;
                case "3": RegistrarReserva(db); break;
                case "4": ListarReservasVigentes(db); break;
                case "5": ReporteUsoCancha(db); break;
                case "0": salir = true; break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    static void RegistrarCancha(AplicationDbContextcs db)
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Tipo (futbol, tenis, padel): ");
        string tipo = Console.ReadLine();

        Console.Write("¿Está activa? (s/n): ");
        bool activa = Console.ReadLine().ToLower() == "s";

        var cancha = new Cancha { Nombre = nombre, Tipo = tipo, Activa = activa };
        db.Canchas.Add(cancha);
        db.SaveChanges();
        Console.WriteLine("Cancha registrada.");
    }

    static void RegistrarSocio(AplicationDbContextcs db)
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Documento: ");
        string documento = Console.ReadLine();

        Console.Write("Correo electrónico: ");
        string correo = Console.ReadLine();

        var socio = new Socio { Nombre = nombre, Documento = documento, CorreoElectronico = correo };
        db.Socios.Add(socio);
        db.SaveChanges();
        Console.WriteLine("Socio registrado.");
    }

    static void RegistrarReserva(AplicationDbContextcs db)
    {
        Console.WriteLine("Seleccione Socio (ingrese ID):");
        foreach (var socio in db.Socios) Console.WriteLine($"{socio.SocioId} - {socio.Nombre}");
        int socioId = int.Parse(Console.ReadLine());

        Console.WriteLine("Seleccione Cancha (ingrese ID):");
        foreach (var cancha in db.Canchas.Where(c => c.Activa)) Console.WriteLine($"{cancha.CanchaId} - {cancha.Nombre}");
        int canchaId = int.Parse(Console.ReadLine());

        Console.Write("Fecha y hora: ");
        DateTime fechaHora = DateTime.Parse(Console.ReadLine());

        bool yaReservada = db.Reservas.Any(r => r.CanchaId == canchaId && r.FechaHora == fechaHora);
        if (yaReservada)
        {
            Console.WriteLine("La cancha ya está reservada en ese horario");
            return;
        }

        var reserva = new Reserva { SocioId = socioId, CanchaId = canchaId, FechaHora = fechaHora };
        db.Reservas.Add(reserva);
        db.SaveChanges();
        Console.WriteLine("Reserva registrada.");
    }

    static void ListarReservasVigentes(AplicationDbContextcs db)
    {
        DateTime ahora = DateTime.Now;

        var reservas = db.Reservas
            .Include(r => r.Socio)
            .Include(r => r.Cancha)
            .Where(r => r.FechaHora.Date == ahora.Date)
            .ToList();

        foreach (var r in reservas)
        {
            string estado = r.FechaHora > ahora ? "Pendiente" :
                            r.FechaHora <= ahora && r.FechaHora.AddHours(1) > ahora ? "En curso" :
                            "Finalizado";

            Console.WriteLine($"{r.ReservaId} - {r.Socio.Nombre} - {r.Cancha.Nombre} - {r.FechaHora} - {estado}");
        }
    }

    static void ReporteUsoCancha(AplicationDbContextcs db)
    {
        var reporte = db.Reservas
            .Include(r => r.Cancha)
            .GroupBy(r => new { r.Cancha.Nombre, r.Cancha.Tipo })
            .Select(g => new
            {
                Cancha = g.Key.Nombre,
                Tipo = g.Key.Tipo,
                Cantidad = g.Count()
            });

        foreach (var item in reporte)
        {
            Console.WriteLine($"{item.Cancha} - {item.Tipo} - {item.Cantidad}");
        }
    }
}


