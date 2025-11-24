using System;
using System.Collections.Generic;

class Empleado
{
    public int Id;
    public string Nombre;
    public bool Activo = true;
}

class Registro
{
    public int EmpleadoId;
    public DateTime Entrada;
    public DateTime? Salida;
}

class Program
{
    static List<Empleado> empleados = new List<Empleado>();
    static List<Registro> registros = new List<Registro>();
    static int nextId = 1; // contador para IDs

    static void Main()
    {
        int opcion;
        do
        {
            Console.WriteLine("=== TempoControl ===");
            Console.WriteLine("1. Crear empleado");
            Console.WriteLine("2. Listar empleados");
            Console.WriteLine("3. Fichar entrada");
            Console.WriteLine("4. Fichar salida (simula 8h)");
            Console.WriteLine("5. Reporte simple");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1: CrearEmpleado(); break;
                case 2: ListarEmpleados(); break;
                case 3: FicharEntrada(); break;
                case 4: FicharSalida(); break;
                case 5: Reporte(); break;
            }
        } while (opcion != 0);
    }

    static void CrearEmpleado()
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        var nuevo = new Empleado { Id = nextId++, Nombre = nombre };
        empleados.Add(nuevo);
        Console.WriteLine($"Empleado creado con ID: {nuevo.Id}\n");
    }

    static void ListarEmpleados()
    {
        foreach (var e in empleados)
            Console.WriteLine($"{e.Id} - {e.Nombre} ({(e.Activo ? "Activo" : "Inactivo")})");
        Console.WriteLine();
    }

    static void FicharEntrada()
    {
        Console.Write("ID empleado: ");
        int id = int.Parse(Console.ReadLine());
        registros.Add(new Registro { EmpleadoId = id, Entrada = DateTime.Now });
        Console.WriteLine("Entrada registrada.\n");
    }

    static void FicharSalida()
    {
        Console.Write("ID empleado: ");
        int id = int.Parse(Console.ReadLine());
        var reg = registros.FindLast(r => r.EmpleadoId == id && r.Salida == null);
        if (reg != null)
        {
            // Simulamos que trabajó 8 horas
            reg.Salida = reg.Entrada.AddHours(8);
            Console.WriteLine("Salida registrada (simulando 8 horas).\n");
        }
        else Console.WriteLine("No hay entrada previa.\n");
    }

    static void Reporte()
    {
        foreach (var e in empleados)
        {
            double horas = 0;
            foreach (var r in registros)
                if (r.EmpleadoId == e.Id && r.Salida != null)
                    horas += (r.Salida.Value - r.Entrada).TotalHours;

            Console.WriteLine($"{e.Nombre} (ID {e.Id}): {horas:F2} horas trabajadas");
        }
        Console.WriteLine();
    }
}