namespace Dominio.Repositorios
{
    public interface IAutenticacionRepository
    {
        Task<string> LoginAsync(string username, string password);
        
    }
}