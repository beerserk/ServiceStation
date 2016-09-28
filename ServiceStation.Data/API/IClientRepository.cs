using ServiceStation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.API
{
    public interface IClientRepository : IDisposable
    {
        IEnumerable<Client> GetClients();

        Client FindClientById(int? clientId);

        void AddClient(Client client);

        void DeleteClient(Client client);

        void UpdateClient(Client client);

        void Save();
    }
}
