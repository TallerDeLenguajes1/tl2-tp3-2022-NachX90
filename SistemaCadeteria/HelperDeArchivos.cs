public static class HelperDeArchivos
{
    public static void LimpiarArchivoTexto(string NombreDelArchivo)
    {
        if (File.Exists("../../../" + NombreDelArchivo))
        {
            using (var FS = new FileStream("../../../" + NombreDelArchivo, FileMode.Truncate))
            {
            }
        }
    }

    public static void EscribirLineaEnArchivo(string NombreDelArchivo, string TextoAGuardar) //SIMEPRE AGREGA. NO BORRA LO ANTERIOR
    {
        using (TextWriter TW = File.AppendText("../../../" + NombreDelArchivo))
        {
            TW.WriteLine(TextoAGuardar);
            TW.Close();
        }
    }

    public static string LeerArchivoTexto(string NombreDelArchivo)
    {
        string TextoLeido;
        using (var FS = new FileStream("../../../" + NombreDelArchivo, FileMode.Open))
        {
            using (var SR = new StreamReader(FS))
            {
                TextoLeido = SR.ReadToEnd();
                FS.Close();
            }
        }
        return TextoLeido;
    }

    public static void LeerCSVYCrearListaDeCadetes(string NombreDelArchivo, List<Cadete> ListaDeCadetes)
    {
        string unaLinea;
        List<Pedido> ListadoDePedidos = new();
        using (var FS = new FileStream("../../../" + NombreDelArchivo, FileMode.Open))
        {
            using (var SR = new StreamReader(FS))
            {
                while ((unaLinea = SR.ReadLine()) != null)
                {
                    var unArray = unaLinea.Split(",");
                    Cadete unCadete = new(Convert.ToInt32(unArray[0]), unArray[1], unArray[2], Convert.ToUInt64(unArray[3]), ListadoDePedidos);
                    ListaDeCadetes.Add(unCadete);

                }
            }
        }
    }
}