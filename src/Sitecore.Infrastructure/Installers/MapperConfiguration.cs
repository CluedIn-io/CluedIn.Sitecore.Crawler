using xConnect = Sitecore.XConnect;
using crawlerModels = CluedIn.Crawling.Sitecore.Core.Models;
using Sitecore.XConnect.Collection.Model;
using AutoMapper;

namespace CluedIn.Crawling.Sitecore.Infrastructure.Installers
{
    public class MapperConfiguration
    {
        public static IConfigurationProvider GetMapperConfiguration() => new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PersonalInformation, crawlerModels.PersonalInfo>();
            cfg.CreateMap<xConnect.Interaction, crawlerModels.Interaction>()
                .ForPath(cmInteraction => cmInteraction.Personal,conf=> conf.Ignore());
            cfg.CreateMap<xConnect.ContactIdentifier, crawlerModels.ContactIdentifier>();
            cfg.CreateMap<xConnect.Contact, crawlerModels.Contact>()
              .ForPath(crawlerContact => crawlerContact.Personal, conf => conf.MapFrom(xconnectContact => xconnectContact.Personal()))
              .ForPath(crawlerContact => crawlerContact.Emails, conf => conf.MapFrom(xconnectContact => xconnectContact.Emails()));
        });
    }
}
