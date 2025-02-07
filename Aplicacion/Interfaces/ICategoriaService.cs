using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaArticulosDto> ObtenerCategoriaPorId(int id);
        Task<List<CategoriaDto>> ObtenerCategorias();
    }
}
