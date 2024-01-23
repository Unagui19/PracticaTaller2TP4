using Entidades;

namespace ManejoDatos
{
    public class AccesoCSV:AccesoADatos 
    {   
        public override List<Cadete> GetCadetes(string route){
            List<Cadete> cadetes = new List<Cadete>();
            
            var csv = new FileStream(route,FileMode.Open);
            var str = new StreamReader(csv);

            while (!str.EndOfStream)
            {
                string linea = str.ReadLine();      
                string[] fields = linea.Split(',');
                cadetes.Add(new Cadete(int.Parse(fields[0]), fields[1], fields[2],fields[3]));
            }
            csv.Close();
            return cadetes;
        }        

        public override Cadeteria GetCadeteria(string route){
            Cadeteria cadeteria = new Cadeteria();
            
            var csv = new FileStream(route,FileMode.Open);
            var str = new StreamReader(csv);

            while (!str.EndOfStream)
            {
                string linea = str.ReadLine();      
                string[] fields = linea.Split(',');
                Cadeteria cadeteria2 = new Cadeteria(fields[0],fields[1]);
                cadeteria=cadeteria2;
            }
            csv.Close();
            return cadeteria;
        }      

    }
}