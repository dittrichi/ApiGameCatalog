using ApiGameCatalog.Entities;
using ApiGameCatalog.Exceptions;
using ApiGameCatalog.InputModel.Publisher;
using ApiGameCatalog.Repositories;
using ApiGameCatalog.ViewModel.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task Create(PublisherInputModel publisherInputModel)
        {
            var publisher = await _publisherRepository.Retrieve(publisherInputModel.Name);

            if (publisher != null)
                throw new PublisherAlreadyExistsException();

            var publisherInsert = new Publisher
            {
                Id = Guid.NewGuid(),
                Name = publisherInputModel.Name
            };

            await _publisherRepository.Insert(publisherInsert);            
        }

        public void Dispose()
        {
            _publisherRepository?.Dispose();
        }

        public async Task Remove(Guid idPublisher)
        {
            var publisher = await _publisherRepository.Retrieve(idPublisher);

            if (publisher == null)
                throw new PublisherNotFoundException();

            await _publisherRepository.Remove(idPublisher);
        }

        public async Task<List<PublisherViewModel>> Retrieve()
        {
            var publisher = await _publisherRepository.Retrieve();

            return publisher.Select(publisher => new PublisherViewModel
            {
                Id = publisher.Id,
                Name = publisher.Name
            }).ToList();
        }

        public async Task<PublisherViewModel> Retrieve(Guid idPublisher)
        {
            var publisher = await _publisherRepository.Retrieve(idPublisher);

            if (publisher == null)
                return null;

            return new PublisherViewModel
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public async Task<PublisherViewModel> Retrieve(string name)
        {
            var publisher = await _publisherRepository.Retrieve(name);

            if (publisher == null)
                return null;

            return new PublisherViewModel
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public async Task Update(Guid idPublisher, string name)
        {
            var publisher = await _publisherRepository.Retrieve(idPublisher);

            if (publisher == null)
                throw new GameNotFoundException();

            await _publisherRepository.Update(idPublisher, name);
        }
    }
}
