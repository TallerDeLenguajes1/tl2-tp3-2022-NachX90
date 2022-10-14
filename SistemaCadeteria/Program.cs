using System;

static class Program
{
    static private NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    static Random aleatorio = new();
    static List<Cadete> ListaDeCadetes = new();
    static List<Cliente> ListaDeClientes = new();
    static List<Pedido> ListaDePedidos = new();
    static int ContadorPedidos = 1;
    static public int Main(string[] args)
    {
        //Inicialización (lectura de csv y generación de listas)
        Cadeteria miCadeteria = IniciarCadeteria();

        Mensajes.Titulo("BIENVENIDO");
        int opcion;
        do
        {
            Mensajes.TerminarLinea("MENU");
            Console.WriteLine("\t[1] Alta de pedidos");
            Console.WriteLine("\t[2] Asignar pedidos a cadetes");
            Console.WriteLine("\t[3] Cambiar estado");
            Console.WriteLine("\t[4] Cambiar cadete");
            Console.WriteLine("\t[5] Mostrar informe y salir");
            Console.Write("Su opción: ");
            try
            {
                opcion = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Opción inválida en el menú");
                opcion = -1;
            }
            switch (opcion)
            {
                case 1:
                    Logger.Info("Menú alta de pedidos");
                    Mensajes.TerminarLinea("Alta de pedidos");
                    AltaPedido();
                    break;
                case 2:
                    Logger.Info("Menú asignar cadete");
                    Mensajes.TerminarLinea("Asignar pedidos a cadetes");
                    AsignarCadete();
                    break;
                case 3:
                    Logger.Info("Menú cambiar estado");
                    Mensajes.TerminarLinea("Cambiar estado");
                    CambiarEstado();
                    break;
                case 4:
                    Logger.Info("Menú cambiar cadete");
                    Mensajes.TerminarLinea("Cambiar cadete");
                    CambiarCadete();
                    break;
                case 5:
                    Logger.Info("Menú seleccion salir");
                    Mensajes.TerminarLinea("Informe final");
                    MostrarInforme();
                    Console.WriteLine("\nGracias por utilizar el programa. Presione una tecla para salir.");
                    Console.ReadKey(true);
                    break;
                default:
                    Logger.Info("Menú seleccion incorrecta");
                    Console.WriteLine("Opción no válida. Intente de nuevo");
                    break;
            }
        } while (opcion != 5);
        Logger.Info("Programa finalizado");
        return 0;
    }

    //Métodos

    static Cadeteria IniciarCadeteria()
    {
        //Generación lista de cadetes
        HelperDeArchivos.LeerCSVYCrearListaDeCadetes("Cadete.csv", ListaDeCadetes);

        //Generación cadetería
        string CSVCadeteria = HelperDeArchivos.LeerArchivoTexto("Cadeteria.csv");
        var ArrayCadeteria = CSVCadeteria.Split(",");
        return new(ArrayCadeteria[0], Convert.ToUInt64(ArrayCadeteria[1]), ListaDeCadetes);
    }

    static void AltaPedido()
    {
        bool seguir = true;
        do
        {
            Logger.Info("Ingreso al alta de pedidos");
            try
            {
                Console.WriteLine("Datos del cliente:");
                Console.Write("\tNombre: ");
                string nombre = Console.ReadLine();
                Console.Write("\tDirección: ");
                string direccion = Console.ReadLine();
                Console.Write("\tObs dirección: ");
                string obsDireccion = Console.ReadLine();
                Console.Write("\tTeléfono: ");
                ulong telefono = Convert.ToUInt64(Console.ReadLine());
                Cliente nuevoCliente = new(ContadorPedidos, nombre, direccion, telefono, obsDireccion);
                ListaDeClientes.Add(nuevoCliente);
                Logger.Info("Alta de cliente exitosa");
                Console.WriteLine("Datos del pedido:");
                Console.Write("\tObservaciones: ");
                string obsPedido = Console.ReadLine();
                Pedido nuevoPedido = new(ContadorPedidos, obsPedido, nuevoCliente, Estado.SinAsignar);
                ListaDePedidos.Add(nuevoPedido);
                ContadorPedidos++;
                Logger.Info("Alta de pedido exitosa");
                seguir = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error en el alta de pedido");
                Console.WriteLine("ERROR: No se pudo cargar el pedido. Ingrese nuevamente");
            }
        } while (seguir);
    }

    static void AsignarCadete()
    {
        Console.WriteLine("Asignación automática de cadetes:");
        //var sinAsignar = ListaDePedidos.Where(p => p.estado == Estado.SinAsignar);
        //foreach (var p in sinAsignar)
        foreach (var p in ListaDePedidos)
        {
            Console.WriteLine($"\nPedido [{p.nro}]");
            Console.WriteLine($"\tCliente:\t{p.cliente.nombre}");
            Console.WriteLine($"\tObservaciones:\t{p.obs}");
            var cadeteACargo = ListaDeCadetes.ElementAt(aleatorio.Next(ListaDeCadetes.Count));
            cadeteACargo.AsignarPedido(p);
            Console.WriteLine($"\tCadete:\t\t{cadeteACargo.nombre}");
        }
        Console.WriteLine("\nLos cadetes fueron asignados correctamente");
        ListaDePedidos.Clear();
    }

    static void CambiarEstado()
    {
        Console.WriteLine("Listado de pedidos según cadetes");
        foreach (var c in ListaDeCadetes)
        {
            Console.WriteLine($"\nCadete [{c.id}]: {c.nombre}");
            foreach (var p in c.listadoPedidos)
            {
                p.ListarPedido();
            }
        }
        Console.Write("Ingrese un número de pedido: ");
        int pedidoBuscado = Convert.ToInt32(Console.ReadLine());
        Console.Write("¿El pedido se entregó? [1]Entregado | [2]Sin Entregar: ");
        int nuevoEstado = Convert.ToInt32(Console.ReadLine());
        foreach (var c in ListaDeCadetes)
        {
            foreach (var p in c.listadoPedidos)
            {
                if (p.nro == pedidoBuscado)
                {
                    if (nuevoEstado == 1)
                    {
                        p.CambiarEstado(Estado.Entregado);
                    }
                    else
                    {
                        p.CambiarEstado(Estado.SinEntregar);
                    }
                }
            }
        }
        Console.WriteLine("Estado cambiado correctamente");
    }

    static void CambiarCadete()
    {
        Pedido pedidoAAsignar = default;
        Console.WriteLine("Listado de pedidos según cadetes");
        foreach (var c in ListaDeCadetes)
        {
            Console.WriteLine($"Cadete [{c.id}]: {c.nombre}");
            foreach (var p in c.listadoPedidos)
            {
                p.ListarPedido();
            }
        }
        Console.Write("Ingrese un número de pedido: ");
        int pedidoBuscado = Convert.ToInt32(Console.ReadLine());
        Console.Write("Ingrese el ID del nuevo cadete: ");
        int nuevoCadete = Convert.ToInt32(Console.ReadLine());
        foreach (var c in ListaDeCadetes)
        {
            foreach (var p in c.listadoPedidos)
            {
                if (p.nro == pedidoBuscado)
                {
                    pedidoAAsignar = c.QuitarPedido(pedidoBuscado);
                }
            }
        }
        foreach (var c in ListaDeCadetes)
        {
            if (c.id == nuevoCadete)
            {
                c.AsignarPedido(pedidoAAsignar);
            }
        }
    }

    static void MostrarInforme()
    {
        decimal pagoTotal = 0;
        foreach (var c in ListaDeCadetes)
        {
            c.ListarCadete();
            pagoTotal += pagoTotal + c.getJornalACobrar();
        }
        Console.WriteLine($"\nPago total:\t{pagoTotal}");
    }
}
