using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaAppRest.Modelos
{
    public class Veiculo
    {
        public int id{ get; set; }
        public string modelo{ get; set; }
        public int ano{ get; set; }
        public int idfabricante{ get; set; }
        public DateTime datacompra{ get; set; }
        public decimal valorcompra{ get; set; }
        public decimal precovenda{ get; set; }
        public DateTime datavenda{ get; set; }
        public decimal valorvenda{ get; set; }
    }
}
