using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Models
{
    public class BaseIdentity
    {
        public int Id { get; set; }
        public bool Isdeleted { get; set; }
    }
}
