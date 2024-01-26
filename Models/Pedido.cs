using System.Dynamic;
using Entidades;

namespace Entidades
{
    public enum Estado {pendiente = 1, asignado = 2, entregado = 3}//, cancelado = 4}
    public class Pedido
    {

        public static int id=0;
        public int Nro { get ; set; }
        public string Obs { get ; set; }
        public Estado Estado { get ; set; }
        public Cliente Cliente { get ; set; }
        public Cadete Cadete {get; set;}

        public Pedido(string obs, string nombreCliente, string DireccionCliente, string TelefonoCliente, string DatosReferenciaDireccion)
        {
            id++;
            Nro = id;
            Obs=obs;
            Estado = Estado.pendiente;
            Cliente = new Cliente(nombreCliente, DireccionCliente, TelefonoCliente, DatosReferenciaDireccion);
        }

        public Pedido (){}

        public Pedido(string obs, Estado estado, Cliente cliente)
        {
            id++;
            Nro = id;
            Obs = obs;
            Estado = estado;
            Cliente = cliente;
        }

        public string verDireccionCliente(){
            return Cliente.Direccion;
        }

        public string verDatosCliente(){
            return Cliente.Nombre + "-" + Cliente.Telefono
             + "-" + Cliente.Direccion + "-" + Cliente.DatosReferenciaDireccion; 
        }

        public void CambiarEstado(int est){
            switch (est)
            {
                case 1: Estado=Estado.pendiente;
                break; 
                case 2: Estado = Estado.asignado;
                break ;
                default: Estado = Estado.entregado;
                        Cadete.pedidoEntregado();
                break ;
                // default: Estado = Estado.cancelado;
                // break;
            }
        }

        public void AsignarCadete(Cadete cadete){
            Cadete=cadete;
            CambiarEstado(2);
        }

        public void DesasignarCadete(){
            Cadete=null;
        }

        

    }
}