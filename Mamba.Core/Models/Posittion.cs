using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Models
{
    public class Posittion:BaseIdentity
    {
        public string Name { get; set; }
        public List<Team>? TeamPosition { get; set; }

    }
}
