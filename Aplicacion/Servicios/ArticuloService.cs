using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;
using Dominio.Repositorios;

public class ArticuloService : IArticuloService
{
    private readonly IArticuloRepository _articuloRepository;

    public ArticuloService(IArticuloRepository articuloRepository)
    {
        _articuloRepository = articuloRepository;
    }

    public async Task<List<ArticuloDto>> ObtenerTodos()
    {
        var articulos = await _articuloRepository.ObtenerTodos();
        return articulos.Select(a => new ArticuloDto
        {
            Id = a.Id,
            Nombre = a.Nombre,
            Cantidad = a.Cantidad,
            CategoriaId = a.CategoriaId,
            CategoriaNombre = a.Categoria.Nombre
        }).ToList();
    }

    public async Task<List<ArticuloDto>> ObtenerPorCategoria(int categoriaId)
    {
        var articulos = await _articuloRepository.ObtenerPorCategoria(categoriaId);
        return articulos.Select(a => new ArticuloDto
        {
            Id = a.Id,
            Nombre = a.Nombre,
            Cantidad = a.Cantidad,
            CategoriaId = a.CategoriaId,
            CategoriaNombre = a.Categoria.Nombre
        }).ToList();
    }

    public async Task<ArticuloDto> ObtenerPorId(int id)
    {
        var articulo = await _articuloRepository.ObtenerPorId(id);
        if (articulo == null) return null;

        return new ArticuloDto
        {
            Id = articulo.Id,
            Nombre = articulo.Nombre,
            Cantidad = articulo.Cantidad,
            CategoriaId = articulo.CategoriaId,
            CategoriaNombre = articulo.Categoria.Nombre
        };
    }

    public async Task<Articulo> Agregar(CrearArticuloDto dto)
    {
        var articulo = new Articulo
        {
            Nombre = dto.Nombre,
            Cantidad = dto.Cantidad,
            CategoriaId = dto.CategoriaId
        };

        return await _articuloRepository.Agregar(articulo);
    }

    public async Task<bool> Actualizar(ActualizarArticuloDto dto)
    {
        var articulo = await _articuloRepository.ObtenerPorId(dto.Id);
        if (articulo == null) return false;

        articulo.Nombre = dto.Nombre;
        articulo.Cantidad = dto.Cantidad;
        articulo.CategoriaId = dto.CategoriaId;

        return await _articuloRepository.Actualizar(articulo);
    }

    public async Task<bool> Eliminar(int id) =>
        await _articuloRepository.Eliminar(id);
}