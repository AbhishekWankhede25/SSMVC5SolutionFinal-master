using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Practices.Unity;
using SSMVC5WebApp.Infrastructure.Abstract;
using SSMVC5WebApp.Infrastructure.Concrete;
using SSMVC5WebApp.Infrastructure.Services;

namespace SSMVC5WebApp.Infrastructure
{
    public class SportsStoreUnityControllerFactory: DefaultControllerFactory
    {
        private UnityContainer _container;
        public SportsStoreUnityControllerFactory()
        {
            _container = new UnityContainer();
            AddBindings();
        }

        private void AddBindings()
        {
            _container.RegisterType<ILogger, Logger>();
            _container.RegisterType<IProductRepository, EfProductRepository>();
            _container.RegisterType<IPhotoService, PhotoService>(new ContainerControlledLifetimeManager());//Singleton
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : _container.Resolve(controllerType) as IController;
            //return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}