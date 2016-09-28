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
    public class ClientRepository : IClientRepository, IDisposable
    {
        private ServiceStationDbContext context;

        public ClientRepository(ServiceStationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Client> GetClients()
        {
            return context.Client.ToList();
        }

        public Client FindClientById(int? clientId)
        {
            return context.Client.Find(clientId);
        }

        public void AddClient(Client client)
        {
            context.Client.Add(client);
        }

        public void DeleteClient(Client client)
        {
            context.Client.Remove(client);
        }

        public void UpdateClient(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
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
