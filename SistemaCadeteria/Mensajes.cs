static class Mensajes
{
    private const int AnchoDeMensajes = 100;
    static public void Titulo(string Titulo)
    {
        int LongitudEspacios = (AnchoDeMensajes - Titulo.Length) / 2;
        string Espacios = "".PadLeft(LongitudEspacios, ' ');
        string LineaMedia = $"║{Espacios}{Titulo.ToUpper()}{Espacios}║";

        Console.WriteLine();
        Console.WriteLine("╔".PadRight(LineaMedia.Length - 1, '═') + "╗");
        Console.WriteLine(LineaMedia);
        Console.WriteLine("╚".PadRight(LineaMedia.Length - 1, '═') + "╝");
    }
    static public void Subtitulo(string Subtitulo)
    {
        int LongitudGuiones = (AnchoDeMensajes - Subtitulo.Length) / 2;
        string Simbolos = "".PadLeft(LongitudGuiones, '~');
        string LineaMedia = $"{Simbolos} {Subtitulo} {Simbolos}";

        Console.WriteLine($"\n{LineaMedia}");
        Console.WriteLine("".PadRight(LineaMedia.Length, '~'));
    }
    static public void CentrarTexto(string Texto)
    {
        Console.WriteLine(Texto.PadLeft((AnchoDeMensajes + Texto.Length) / 2, ' '));
    }
    static public void TerminarLinea(string Texto)
    {
        string GuionesIzquierda = "═══";
        string GuionesDerecha = "".PadRight(AnchoDeMensajes - GuionesIzquierda.Length - Texto.Length, '═');
        Console.WriteLine($"\n{GuionesIzquierda} {Texto} {GuionesDerecha}");
    }
}
