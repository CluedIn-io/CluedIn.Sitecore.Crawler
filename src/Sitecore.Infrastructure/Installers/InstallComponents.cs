using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;

using CluedIn.Core;
using CluedIn.Crawling.Sitecore.Infrastructure.Factories;
using AutoMapper;

namespace CluedIn.Crawling.Sitecore.Infrastructure.Installers
{
  public class InstallComponents : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container
          .AddFacilityIfNotExists<TypedFactoryFacility>()
          .Register(Component.For<ISitecoreClientFactory>().AsFactory())
          .Register(Component.For<SitecoreClient>().LifestyleTransient())
          .Register(Component.For<IMapper>().Instance(CreateAutoMapper())
          );
    }

    private IMapper CreateAutoMapper() => new Mapper(MapperConfiguration.GetMapperConfiguration());
    
  }
}
