public class Cliente : Persona
{
    public string datosReferenciaDireccion { get; private set; }

    public Cliente (int id, string nombre, string direccion, ulong telefono, string datosReferenciaDireccion) : base(id, nombre, direccion, telefono)
    {
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }
}