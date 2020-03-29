using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class Status
    {
        public int    IdStatus          { get; set; }

        public string DescricaoStatus   { get; set; } 

        public Status() { }

        public Status (int id)
        {
            this.IdStatus = id;
        }

        public Status (string status)
        {
            this.DescricaoStatus = status;
        }

        public Status (int id, string status)
        {
            this.IdStatus        = id;
            this.DescricaoStatus = status;
        }
    }
}
