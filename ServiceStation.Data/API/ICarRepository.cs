using ServiceStation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.API
{
    public interface ICarRepository : IDisposable
    {
        IEnumerable<Car> GetCars();

        Car FindCarById(int? carId);

        void AddCar(Car car);

        void DeleteCar(Car car);

        void UpdateCar(Car car);

        void Save();
    }
}
