using ApiGameCatalog.InputModel.Publisher;
using ApiGameCatalog.ViewModel.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public interface IPublisherService : IDisposable
    {
        Task<List<PublisherViewModel>> Retrieve();
        Task<PublisherViewModel> Retrieve(Guid idPublisher);
        Task<PublisherViewModel> Retrieve(string name);
        Task Create(PublisherInputModel publisherInputModel);
        Task Update(Guid idPublisher, string name);
        Task Remove(Guid idPublisher);
    }
}
