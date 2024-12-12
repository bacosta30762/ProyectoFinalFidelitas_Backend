using Dominio.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IMotorDePlantillas
    {
        string ObtenerPlantilla(Notificacion notificacion);
        Task<string> RenderizarPlantillaAsync<T>(string templateName, T model);
    }
}
