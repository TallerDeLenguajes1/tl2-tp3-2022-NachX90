public class Cadete : Persona
{
    private const decimal COMISION = 300;
    public List<Pedido> listadoPedidos { get; private set; }

    public Cadete (int id, string nombre, string direccion, ulong telefono, List<Pedido> pedidos) : base(id, nombre, direccion, telefono)
    {
        listadoPedidos = pedidos;
    }

    public decimal getJornalACobrar()
    {
        return listadoPedidos.Where(x => x.estado == Estado.Entregado).Count() * COMISION;
    }

    public void AsignarPedido(Pedido pedido)
    {
        pedido.CambiarEstado(Estado.Asignado);
        listadoPedidos.Add(pedido);
    }

    public Pedido QuitarPedido(int id)
    {
        Pedido pedidoAQuitar = listadoPedidos.Single(p => p.nro == id);
        listadoPedidos.Remove(pedidoAQuitar);
        return pedidoAQuitar;
    }

    public void ListarCadete()
    {
        Console.WriteLine($"\nCadete [{id}]");
        Console.WriteLine($"\tNombre:\t\t\t{nombre}");
        Console.WriteLine($"\tCant pedidos totales:\t{listadoPedidos.Count}");
        Console.WriteLine($"\tCant pedidos entregados:\t{listadoPedidos.Where(x => x.estado == Estado.Entregado).Count()}");
        Console.WriteLine($"\tComisión por entregados:\t{getJornalACobrar()}");
    }
}