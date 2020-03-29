using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class ColetaVisualiza
    {
        public int      IdVisualiza      { get; set; }
        
        public int      IdColeta         { get; set; }
        public int      IdMotorista      { get; set; }
        public int      IdCliente        { get; set; }
        public DateTime DataVisualizacao { get; set; }

        public ColetaVisualiza() { }

        public ColetaVisualiza(int idVisualiza)
        {
            this.IdVisualiza = idVisualiza;
        }

        public ColetaVisualiza(int idColeta, int idMotorista)
        {
            this.IdColeta    = idColeta;
            this.IdMotorista = IdMotorista;
        }
        
    }
}
