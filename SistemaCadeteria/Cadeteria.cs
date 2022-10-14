public class Cadeteria
{
    public string nombre { get; private set; }
    public ulong telefono { get; private set; }
    public List<Cadete> listadoCadetes { get; private set; }

    public Cadeteria(string nombre, ulong telefono, List<Cadete> listadoCadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listadoCadetes = listadoCadetes;
    }
}