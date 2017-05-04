﻿using System;
using System.IdentityModel.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using Kentor.AuthServices;
using Kentor.AuthServices.Configuration;
using Kentor.AuthServices.Owin;
using Kentor.AuthServices.WebSso;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(TestClient.Saml2p.Startup))]

namespace TestClient.Saml2p
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions {AuthenticationType = "cookie"});

            var options = new KentorAuthServicesAuthenticationOptions(false)
            {
                SPOptions = new SPOptions {EntityId = new EntityId("http://localhost:4100/AuthServices"), },
                AuthenticationType = "saml2p",
                SignInAsAuthenticationType = "cookie"
            };


            var idp = new IdentityProvider(new EntityId("http://stubidp.kentor.se/Metadata"), options.SPOptions)
            {
                AllowUnsolicitedAuthnResponse = true,
                Binding = Saml2BindingType.HttpPost,
                SingleSignOnServiceUrl = new Uri("http://stubidp.kentor.se")
            };
            idp.SigningKeys.AddConfiguredKey(
                new X509Certificate2(HostingEnvironment.MapPath("~/Kentor.AuthServices.StubIdp.cer")));
            options.IdentityProviders.Add(idp);

            app.UseKentorAuthServicesAuthentication(options);
        }
    }
}
