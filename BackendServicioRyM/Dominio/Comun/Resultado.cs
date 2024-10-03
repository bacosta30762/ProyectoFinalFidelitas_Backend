namespace Dominio.Comun
{
    public class Resultado
    {
        public bool FueExitoso { get; set; }

        public IEnumerable<string> Errores { get; set; } = [];

        public static Resultado Exitoso()
        {
            return new Resultado {FueExitoso = true};
        }

        public static Resultado Fallido(IEnumerable<string> errores)
        {
            return new Resultado { FueExitoso = false, Errores = errores };
        }

    }

    public class Resultado<Tipo> : Resultado
    {
        public Tipo? Valor { get; set; }

        
    }
}
