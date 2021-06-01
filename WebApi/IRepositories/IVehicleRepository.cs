using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.IRepositories
{
    public interface IVehicleRepository
    {
        Vehicle Get(int id);
    }
}
