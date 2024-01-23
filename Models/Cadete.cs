using Entidades;

namespace Entidades
{
    public class Cadete
    {
        int id;
        string nombre;
        string direccion;
        string telefono;
        int cantPedidosEntregados;


        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int CantPedidosEntregados { get => cantPedidosEntregados; set => cantPedidosEntregados = value; }


        public Cadete(){}
        public Cadete(int id, string nombre, string direccion, string telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.cantPedidosEntregados = 0;
        }


        public string MostrarInfo(){
            return "Id : "+ id + "- Nombre : " + nombre + "\n"; 
        }

        public void pedidoEntregado(){
            cantPedidosEntregados++;
        }
       
    }
    
}