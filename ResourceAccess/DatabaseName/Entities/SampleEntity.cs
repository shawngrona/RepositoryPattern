using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceAccess.DatabaseName.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    public partial class SampleEntity
    {
        public int theInt { get; set; }
        public string theString { get; set; }
 
    }
}
