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

    public class Resultado<T> : Resultado
    {
        public T Valor { get; set; } = default;

        public static Resultado<T> Exitoso(T valor)
        {
            return new Resultado<T> { FueExitoso = true, Valor = valor };
        }

        public static new Resultado<T> Fallido(IEnumerable<string> errores)
        {
            return new Resultado<T> { FueExitoso = false, Errores = errores };
        }


    }
}
