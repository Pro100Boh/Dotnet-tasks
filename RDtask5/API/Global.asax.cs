using API.Controllers;
using BLL.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //IKernel kernel = new StandardKernel(new CatalogModule());
            //kernel.Get<ProductsController>();
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
