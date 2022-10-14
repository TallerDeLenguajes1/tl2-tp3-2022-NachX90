public abstract class Persona
{
    public int id { get; private set; }
    public string nombre { get; private set; }
    public string direccion { get; private set; }
    public ulong telefono { get; private set; }

    public Persona (int id, string nombre, string direccion, ulong telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }
}