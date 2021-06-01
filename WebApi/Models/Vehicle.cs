using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public abstract class Base
    {
        public int Id { get; set; }
    }

    public class Vehicle : Base
    {
        public string Name { get; set; }
        public string Model { get; set; }
    }
}
