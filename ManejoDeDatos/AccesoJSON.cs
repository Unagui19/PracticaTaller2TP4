using Entidades;
using ManejoDatos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManejoDatos
{
    public class AccesoJson: AccesoADatos
    {
        public override Cadeteria GetCadeteria(string route)
        {

                string textoJson = File.ReadAllText(route);
                Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(textoJson);
                return cadeteria;
        }

        public override List<Cadete> GetCadetes(string route)
        {
             string textoJson = File.ReadAllText(route);
                List<Cadete> nuevaLista = JsonSerializer.Deserialize<List<Cadete>>(textoJson);
                return nuevaLista;
        }
        
    }
    public class AccesoADatosCadeteria:AccesoJson
    {
        public Cadeteria Obtener()
        {
            return GetCadeteria("data/cadeteria.json");
        }

    }

    public class AccesoADatosCadetes:AccesoJson
    {
        public List<Cadete> Obtener()
        {
            return GetCadetes("data/Cadetes.json");
        }

    }

    public class AccesoADatosPedidos:AccesoJson
    {
        public void guardarPedidos(List<Pedido> pedidos)
        {    
            var fst = new FileStream("data/Pedidos.json",FileMode.OpenOrCreate);
            var options = new JsonSerializerOptions { WriteIndented = true };//para que quede bien identado el json
            string archivoJson = JsonSerializer.Serialize(pedidos,options);// se pone la opcion aqui
            using (var sw =new StreamWriter(fst))
            {
                sw.WriteLine(archivoJson);
                sw.Close();
            }//PARA CREAR UN JSON y guardar o solo guardar 
            fst.Close();
        }
        public List<Pedido> Obtener()
        {
            string textoJson = File.ReadAllText("data/Pedidos.json");
            List<Pedido> pedidos= JsonSerializer.Deserialize<List<Pedido>>(textoJson); 
            // Console.WriteLine($"{cadeteria.Nombre},{cadeteria.Telefono},{cadeteria.Code}");
            return pedidos; 
        }

    }
    
}