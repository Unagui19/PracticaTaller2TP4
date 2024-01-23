using Entidades;
using ManejoDatos;

namespace ManejoDatos
{
    public abstract class AccesoADatos
    {   
        public abstract List<Cadete> GetCadetes(string route);            
        public abstract Cadeteria GetCadeteria(string route);

    }
}