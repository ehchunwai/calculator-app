using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_app.Models
{
    public class User
    {
        public Guid Uid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string ID { get; set; }
    }
}
