using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallJson.Models
{
    public class PersonPet
    {
        public string Gender { get; set; }
        public List<Pet> Cats { get; set; }
    }
}
