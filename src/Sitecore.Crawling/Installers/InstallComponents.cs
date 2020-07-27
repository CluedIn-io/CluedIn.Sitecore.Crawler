using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Crawling.Sitecore.Vocabularies;

namespace CluedIn.Crawling.Sitecore.Installers
{
    // Use this class to add any further dependencies to the container

    public class InstallComponents : IWindsorInstaller
    {
        public void Install([NotNull] IWindsorContainer container, [NotNull] IConfigurationStore store)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (store == null)
                throw new ArgumentNullException(nameof(store));

            container
              .Register(Component.For<ContactVocabulary>().Named("ContactVocabularyOverride"))
              .Register(Component.For<InteractionVocabulary>().Named("InteractionVocabularyOverride"))
                ;
        }
    }
}
