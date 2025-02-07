using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class CategoriaService : ICategoriaService
    {

    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService (ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

        public async Task<List<CategoriaDto>> ObtenerCategorias()
        {
            var categorias = await _categoriaRepository.ObtenerTodas();
            return categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre
            }).ToList();
        }

        public async Task<CategoriaArticulosDto> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _categoriaRepository.ObtenerPorId(id);
            if (categoria == null) return null;

            return new CategoriaArticulosDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Articulos = categoria.Articulos.Select(a => a.Nombre).ToList()
            };
        }

    }
}
