using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ApiGameCatalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LifeCycleIDController : ControllerBase
    {
        //To execute the test, just run the application and go to https://localhost:44322/api/v1/LifeCycleID
        //When refreshing browser, singleton will not change, scoped will change  but always with the same ID per requisition
        //and transiente will always be different in each requisition 

        //Create when the application runs, in memory, instances lasting until the end of project
        public readonly ISingletonSample _singletonSample1;
        public readonly ISingletonSample _singletonSample2;

        //An instance that will last through requisition lifetime
        public readonly IScopedSample _scopedSample1;
        public readonly IScopedSample _scopedSample2;

        //Every new injection will get a new intance
        public readonly ITransientSample _transientSample1;
        public readonly ITransientSample _transientSample2;

        public LifeCycleIDController(ISingletonSample sampleSingleton1,
                                       ISingletonSample sampleSingleton2,
                                       IScopedSample sampleScoped1,
                                       IScopedSample sampleScoped2,
                                       ITransientSample sampleTransient1,
                                       ITransientSample sampleTransient2)
        {
            _singletonSample1 = sampleSingleton1;
            _singletonSample2 = sampleSingleton2;
            _scopedSample1 = sampleScoped1;
            _scopedSample2 = sampleScoped2;
            _transientSample1 = sampleTransient1;
            _transientSample2 = sampleTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_singletonSample1.Id}");
            stringBuilder.AppendLine($"Singleton 2: {_singletonSample2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_scopedSample1.Id}");
            stringBuilder.AppendLine($"Scoped 2: {_scopedSample2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_transientSample1.Id}");
            stringBuilder.AppendLine($"Transient 2: {_transientSample2.Id}");

            return Task.FromResult(stringBuilder.ToString());
        }

    }

    public interface IGeneralSample
    {
        public Guid Id { get; }
    }

    public interface ISingletonSample : IGeneralSample
    { }

    public interface IScopedSample : IGeneralSample
    { }

    public interface ITransientSample : IGeneralSample
    { }

    public class LifeCycleSample : ISingletonSample, IScopedSample, ITransientSample
    {
        private readonly Guid _guid;

        public LifeCycleSample()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Id => _guid;
    }

}
