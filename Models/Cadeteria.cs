using System.ComponentModel;
using System.Linq;
using Entidades;
using ManejoDatos;
using System.Text;
// using APICadeteria;

namespace Entidades
{
    public class Cadeteria
    {

        public string Nombre {get; set;}
        public string Telefono {get; set;}
        public List<Cadete> Cadetes {get; set;}
        public List<Pedido> ListadoPedidos {get; set;} 
        public int CantidadDePedidos {get; set;}
        // AccesoADatos HelperPedidos = new();

        //PATRON SINGLETON
        private static Cadeteria instancia;

        public static Cadeteria GetInstancia(){
            if (instancia == null)
            {
                AccesoADatos helperJson = new AccesoJson();
                instancia = helperJson.GetCadeteria("data/Cadeteria.json");
                instancia.Cadetes = helperJson.GetCadetes("data/Cadetes.json");
            }
            return instancia;
        }


        public Cadeteria(){

        }


        public Cadeteria(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
            Cadetes = new List<Cadete>();
            ListadoPedidos = new List<Pedido>();
            CantidadDePedidos = 0;
        }


        public string Mostrar(){
            return "Cadeteria "+ Nombre + "- Telefono : " + Telefono + "- Cantidad de cadetes : " + Cadetes.Count() + "\n"; 
        }   

        // public Pedido CrearPedido(string obs, string nombreCliente, string DireccionCliente, string TelefonoCliente, string DatosReferenciaDireccion){

        //     Pedido pedido = new Pedido(obs,nombreCliente,DireccionCliente,TelefonoCliente,DatosReferenciaDireccion);
        //     ListadoPedidos.Add(pedido);
        //     return pedido;
        // }

        public bool AgregarPedido(Pedido nuevoPedido){
            if (nuevoPedido == null) return false;

            CantidadDePedidos++;
            nuevoPedido.Nro = CantidadDePedidos;      
        if (ListadoPedidos == null) {
        ListadoPedidos = new List<Pedido>();
        }      
            ListadoPedidos.Add(nuevoPedido);
            return true;    
        }


        public void AsignarCadeteAPedido(int nroPedido, int idCadete){
            Cadete cadete = BuscarCadetePorId(idCadete);
            if (cadete!=null)
            {
                Pedido pedido = BuscarPedido(nroPedido);
                if (pedido!=null)
                {
                    pedido.AsignarCadete(cadete);
                }
            }
        }

        public Cadete BuscarCadetePorId(int idCadete){
            Cadete cadete = Cadetes.FirstOrDefault(cad => cad.Id == idCadete);
            if (cadete!=null)
            {
                return cadete;            
            }
            else
            {
                return null;
            }
        }


        public void ConfirmarEntrega(Pedido pedido){
            if (pedido.Cadete!=null)
            {
                pedido.CambiarEstado(3);                
            }
        }


        public void ReasignarPedido(int idCadete, int numeroPedido){
            Pedido pedido = BuscarPedido(numeroPedido);
            if (pedido != null){
                pedido.DesasignarCadete();
                pedido.AsignarCadete(BuscarCadetePorId(idCadete));
            }
        }


        
        public void CambiarEstado(int estado, int numeroPedido){
            Pedido pedido = BuscarPedido(numeroPedido);

                if (pedido != null)
                {
                    if (estado == 4)
                    {
                        ListadoPedidos.Remove(pedido);
                    }
                    else if(estado == 3){
                        ConfirmarEntrega(pedido);
                    }
                    else{
                        pedido.CambiarEstado(estado);                
                    }
                }
        }

        public Pedido BuscarPedido(int nroPedido){
            Pedido pedido = ListadoPedidos.FirstOrDefault(ped => ped.Nro == nroPedido);
            if (pedido != null)
            {
                return pedido;            
            }
            else{
                return null;
            }
        }

        public double JornalACobrar(int idCadete){
            Cadete cadete = BuscarCadetePorId(idCadete);
            if (cadete != null)
            {

                return 500 * cadete.CantPedidosEntregados;
            }
            else
            {
                return -1;
            }

        }
        
        public string GetInforme()
        {
            double montoTotal = 0;
            int totalEnvios = 0;
            string informe = "Informe de Pedidos:\n";

            foreach (Cadete cadete in Cadetes)
            {
                int enviosCadete = cadete.CantPedidosEntregados;
                double montoCadete = JornalACobrar(cadete.Id);

                informe += $"Cadete: {cadete.Nombre}\n";
                informe += $"Cantidad de Envíos: {enviosCadete}\n";
                informe += $"Monto Ganado: ${montoCadete}\n\n";

                totalEnvios += enviosCadete;
                montoTotal += montoCadete;
            }

            double promedioEnviosPorCadete = (double)totalEnvios / Cadetes.Count;

            informe += "Resumen General:\n";
            informe += $"Total de Envíos: {totalEnvios}\n";
            informe += $"Monto Total Ganado: ${montoTotal}\n";
            informe += $"Cantidad Promedio de Envíos por Cadete: {promedioEnviosPorCadete:F2}";

            return informe;
        }

    }
}