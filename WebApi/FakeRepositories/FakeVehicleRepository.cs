using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.IRepositories;
using WebApi.Models;

namespace WebApi.FakeRepositories
{
    public class FakeVehicleRepository : IVehicleRepository
    {
        public Vehicle Get(int id)
        {
            return new Vehicle { Id = 1, Model = "Mazda", Name = "6" };
        }
    }
}
