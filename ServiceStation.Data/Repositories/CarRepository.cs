using ServiceStation.Data.API;
using ServiceStation.Data.Context;
using ServiceStation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.Repositories
{
    public class CarRepository : ICarRepository, IDisposable
    {
        private ServiceStationDbContext context;

        public CarRepository(ServiceStationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Car> GetCars()
        {
            var cars = context.Car.Include(c => c.Owner);
            return cars.ToList();
        }

        public Car FindCarById(int? carId)
        {
            return context.Car.Find(carId);
        }

        public void AddCar(Car car)
        {
            context.Car.Add(car);
        }

        public void DeleteCar(Car car)
        {
            context.Car.Remove(car);
        }

        public void UpdateCar(Car car)
        {
            context.Entry(car).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
