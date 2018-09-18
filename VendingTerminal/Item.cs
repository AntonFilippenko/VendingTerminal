using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingTerminal
{
    class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }

        [JsonIgnore]
        public string Info {
            get {
                return $"{Name} - {Price} RUB";
            }
        }
    }
}
