﻿using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace AuthServer.Host
{
    public class AuthServerDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        public AuthServerDataSeeder(
            IClientRepository clientRepository,
            IApiResourceRepository apiResourceRepository,
            IApiScopeRepository apiScopeRepository,
            IIdentityResourceDataSeeder identityResourceDataSeeder,
            IGuidGenerator guidGenerator,
            IPermissionDataSeeder permissionDataSeeder)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;
            _apiScopeRepository = apiScopeRepository;
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await _identityResourceDataSeeder.CreateStandardResourcesAsync();
            await CreateApiResourcesAsync();
            await CreateApiScopesAsync();
            await CreateClientsAsync();
        }

        private async Task CreateApiScopesAsync()
        {
            await CreateApiScopeAsync("BaseService");
            await CreateApiScopeAsync("InternalGateway");
            await CreateApiScopeAsync("WebAppGateway");
            await CreateApiScopeAsync("BusinessService");
            await CreateApiScopeAsync("AAAService");
            await CreateApiScopeAsync("PeBusiness");
            await CreateApiScopeAsync("FeedBackService");
        }

        private async Task CreateApiResourcesAsync()
        {
            var commonApiUserClaims = new[]
            {
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role"
            };

            await CreateApiResourceAsync("BaseService", commonApiUserClaims);
            await CreateApiResourceAsync("InternalGateway", commonApiUserClaims);
            await CreateApiResourceAsync("WebAppGateway", commonApiUserClaims);
            await CreateApiResourceAsync("BusinessService", commonApiUserClaims);
            await CreateApiResourceAsync("AAAService", commonApiUserClaims);
            await CreateApiResourceAsync("PeBusiness", commonApiUserClaims);
            await CreateApiResourceAsync("FeedBackService", commonApiUserClaims);
        }

        private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(name);
            if (apiResource == null)
            {
                apiResource = await _apiResourceRepository.InsertAsync(
                    new ApiResource(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            foreach (var claim in claims)
            {
                if (apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(apiResource);
        }

        private async Task<ApiScope> CreateApiScopeAsync(string name)
        {
            var apiScope = await _apiScopeRepository.FindByNameAsync(name);
            if (apiScope == null)
            {
                apiScope = await _apiScopeRepository.InsertAsync(
                    new ApiScope(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            return apiScope;
        }

        private async Task CreateClientsAsync()
        {
            var commonScopes = new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address"
            };

            await CreateClientAsync(
                    name: "blazor-app",
                    scopes: commonScopes.Append("BaseService").Append("WebAppGateway").Append("BusinessService"),
                    grantTypes: new[] { "authorization_code" },
                    secret: null,
                    requireClientSecret: false,
                    redirectUri: $"http://localhost:44307/authentication/login-callback",
                    postLogoutRedirectUri: $"http://localhost:44307/authentication/logout-callback",
                    corsOrigins: new[] { "http://localhost:44307", "http://10.221.128.160:44307" }
                );

            await CreateClientAsync(
                name: "basic-web",
                scopes: commonScopes.Append("BaseService").Append("WebAppGateway").Append("BusinessService").Append("AAAService").Append("PeBusiness").Append("FeedBackService"),
                grantTypes: new[] { "password" },
                secret: null,
                requireClientSecret: false
            );

            await CreateClientAsync(
                name: "business-app",
                scopes: new[] { "InternalGateway", "BaseService" },
                grantTypes: new[] { "client_credentials" },
                secret: "1q2w3e*".Sha256(),
                permissions: new[] { IdentityPermissions.Users.Default, IdentityPermissions.UserLookup.Default },
                    corsOrigins: new[] { "http://localhost:51186", "http://10.221.128.160:51186" }

            );
            await CreateClientAsync(
                name: "aaa-app",
                scopes: new[] { "InternalGateway", "BaseService" },
                grantTypes: new[] { "client_credentials" },
                secret: "1q2w3e*".Sha256(),
                permissions: new[] { IdentityPermissions.Users.Default, IdentityPermissions.UserLookup.Default },
                    corsOrigins: new[] { "http://localhost:44308", "http://10.221.128.160:44308" }

            );
            await CreateClientAsync(
                name: "feedback-app",
                scopes: new[] { "InternalGateway", "BaseService", "FeedBackService" },
                grantTypes: new[] { "client_credentials" },
                secret: "1q2w3e*".Sha256(),
                permissions: new[] { IdentityPermissions.Users.Default, IdentityPermissions.UserLookup.Default, "FeedBack.ProgramInfo", "FeedBack.ShapeInfo" },
                    corsOrigins: new[] { "http://localhost:44309", "http://10.221.128.160:44309" }

            );
            await CreateClientAsync(
                name: "pebusiness-app",
                scopes: new[] { "InternalGateway", "BaseService" },
                grantTypes: new[] { "client_credentials" },
                secret: "1q2w3e*".Sha256(),
                permissions: new[] { IdentityPermissions.Users.Default, IdentityPermissions.UserLookup.Default },
                    corsOrigins: new[] { "http://localhost:44351", "http://10.221.128.160:44351" }

            );
            await CreateClientAsync(
                name: "PeBusiness_Swagger",
                scopes: commonScopes.Append("BaseService").Append("WebAppGateway").Append("PeBusiness"),
                grantTypes: new[] { "authorization_code" },
                secret: "1q2w3e*".Sha256(),
                redirectUri: "http://localhost:44351/swagger/oauth2-redirect.html",
                permissions: new[] { IdentityPermissions.Users.Default, IdentityPermissions.UserLookup.Default },
                corsOrigins: new[] { "http://localhost:44351", "http://10.221.128.160:44351" }
            );
            await CreateClientAsync(
                name: "AAA_Swagger",
                scopes: commonScopes.Append("BaseService").Append("WebAppGateway").Append("AAAService"),
                grantTypes: new[] { "authorization_code" },
                secret: "1q2w3e*".Sha256(),
                redirectUri: "http://localhost:44308/swagger/oauth2-redirect.html",
                permissions: new[] { IdentityPermissions.Users.Default, IdentityPermissions.UserLookup.Default },
                corsOrigins: new[] { "http://localhost:44308", "http://10.221.128.160:44308" }
            );
            await CreateClientAsync(
                name: "FeedBack_Swagger",
                scopes: commonScopes.Append("BaseService").Append("WebAppGateway").Append("FeedBackService"),
                grantTypes: new[] { "authorization_code" },
                secret: "1q2w3e*".Sha256(),
                redirectUri: "http://localhost:44309/swagger/oauth2-redirect.html",
                permissions: new[] { IdentityPermissions.Users.Default, IdentityPermissions.UserLookup.Default },
                corsOrigins: new[] { "http://localhost:44309", "http://10.221.128.160:44309" }
            );
        }

        private async Task<Client> CreateClientAsync(
            string name,
            IEnumerable<string> scopes,
            IEnumerable<string> grantTypes,
            string secret = null,
            string redirectUri = null,
            string postLogoutRedirectUri = null,
            string frontChannelLogoutUri = null,
            bool requireClientSecret = true,
            bool requirePkce = false,
            IEnumerable<string> permissions = null,
            IEnumerable<string> corsOrigins = null)
        {
            var client = await _clientRepository.FindByClientIdAsync(name);
            if (client == null)
            {
                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        name
                    )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false,
                        FrontChannelLogoutUri = frontChannelLogoutUri,
                        RequireClientSecret = requireClientSecret,
                        RequirePkce = requirePkce
                    },
                    autoSave: true
                );
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (!secret.IsNullOrEmpty())
            {
                if (client.FindSecret(secret) == null)
                {
                    client.AddSecret(secret);
                }
            }

            if (redirectUri != null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    ClientPermissionValueProvider.ProviderName,
                    name,
                    permissions,
                    null
                );
            }

            if (corsOrigins != null)
            {
                foreach (var origin in corsOrigins)
                {
                    if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                    {
                        client.AddCorsOrigin(origin);
                    }
                }
            }

            return await _clientRepository.UpdateAsync(client);
        }
    }
}
