public class Pedido
{
    public int nro { get; private set; }
    public string obs { get; private set; }
    public Cliente cliente { get; private set; }
    public Estado estado { get; private set; }

    public Pedido (int nro, string obs, Cliente cliente, Estado estado)
    {
        this.nro = nro;
        this.obs = obs;
        this.cliente = cliente;
        this.estado = estado;
    }

    public void CambiarEstado(Estado nuevoEstado)
    {
        estado = nuevoEstado;
    }

    public void ListarPedido()
    {
        Console.WriteLine($"\nPedido [{nro}]");
        Console.WriteLine($"\tCliente:\t{cliente.nombre}");
        Console.WriteLine($"\tObservaciones:\t{obs}");
        Console.WriteLine($"\tEstado:\t\t{estado.ToString()}");
    }
}