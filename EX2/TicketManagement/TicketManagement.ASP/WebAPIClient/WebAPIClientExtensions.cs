﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace TicketManagement.ASP.WebAPI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for WebAPIClient.
    /// </summary>
    public static partial class WebAPIClientExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='userInfo'>
            /// </param>
            public static string ApiAccountTokenGet(this IWebAPIClient operations, UserInfo userInfo = default(UserInfo))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountTokenGetAsync(userInfo), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='userInfo'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<string> ApiAccountTokenGetAsync(this IWebAPIClient operations, UserInfo userInfo = default(UserInfo), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountTokenGetWithHttpMessagesAsync(userInfo, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<ApplicationUser> ApiAccountAllGet(this IWebAPIClient operations)
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountAllGetAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<ApplicationUser>> ApiAccountAllGetAsync(this IWebAPIClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountAllGetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static IList<string> ApiAccountGetRolesGet(this IWebAPIClient operations, string id = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountGetRolesGetAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<string>> ApiAccountGetRolesGetAsync(this IWebAPIClient operations, string id = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountGetRolesGetWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='role'>
            /// </param>
            public static bool? ApiAccountAddToRolePost(this IWebAPIClient operations, string id = default(string), string role = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountAddToRolePostAsync(id, role), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='role'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountAddToRolePostAsync(this IWebAPIClient operations, string id = default(string), string role = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountAddToRolePostWithHttpMessagesAsync(id, role, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='role'>
            /// </param>
            public static bool? ApiAccountRemoveFronRoleDelete(this IWebAPIClient operations, string id = default(string), string role = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountRemoveFronRoleDeleteAsync(id, role), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='role'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountRemoveFronRoleDeleteAsync(this IWebAPIClient operations, string id = default(string), string role = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountRemoveFronRoleDeleteWithHttpMessagesAsync(id, role, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static bool? ApiAccountRemoveUserDelete(this IWebAPIClient operations, string id = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountRemoveUserDeleteAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountRemoveUserDeleteAsync(this IWebAPIClient operations, string id = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountRemoveUserDeleteWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='model'>
            /// </param>
            public static bool? ApiAccountRegisterPost(this IWebAPIClient operations, RegisterModel model = default(RegisterModel))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountRegisterPostAsync(model), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='model'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountRegisterPostAsync(this IWebAPIClient operations, RegisterModel model = default(RegisterModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountRegisterPostWithHttpMessagesAsync(model, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='model'>
            /// </param>
            public static RegisterResult ApiAccountRegisterForIdResultPost(this IWebAPIClient operations, RegisterModel model = default(RegisterModel))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountRegisterForIdResultPostAsync(model), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='model'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<RegisterResult> ApiAccountRegisterForIdResultPostAsync(this IWebAPIClient operations, RegisterModel model = default(RegisterModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountRegisterForIdResultPostWithHttpMessagesAsync(model, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='login'>
            /// </param>
            public static string ApiAccountLoginPost(this IWebAPIClient operations, LoginModel login = default(LoginModel))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountLoginPostAsync(login), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='login'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<string> ApiAccountLoginPostAsync(this IWebAPIClient operations, LoginModel login = default(LoginModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountLoginPostWithHttpMessagesAsync(login, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='token'>
            /// </param>
            /// <param name='password'>
            /// </param>
            /// <param name='newPassword'>
            /// </param>
            public static bool? ApiAccountChangePasswordPut(this IWebAPIClient operations, string token = default(string), string password = default(string), string newPassword = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountChangePasswordPutAsync(token, password, newPassword), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='token'>
            /// </param>
            /// <param name='password'>
            /// </param>
            /// <param name='newPassword'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountChangePasswordPutAsync(this IWebAPIClient operations, string token = default(string), string password = default(string), string newPassword = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountChangePasswordPutWithHttpMessagesAsync(token, password, newPassword, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='token'>
            /// </param>
            /// <param name='email'>
            /// </param>
            public static bool? ApiAccountChangeEmailPut(this IWebAPIClient operations, string token = default(string), string email = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountChangeEmailPutAsync(token, email), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='token'>
            /// </param>
            /// <param name='email'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountChangeEmailPutAsync(this IWebAPIClient operations, string token = default(string), string email = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountChangeEmailPutWithHttpMessagesAsync(token, email, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='email'>
            /// </param>
            public static bool? ApiAccountChangeUserEmailPut(this IWebAPIClient operations, string id = default(string), string email = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountChangeUserEmailPutAsync(id, email), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='email'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountChangeUserEmailPutAsync(this IWebAPIClient operations, string id = default(string), string email = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountChangeUserEmailPutWithHttpMessagesAsync(id, email, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='token'>
            /// </param>
            /// <param name='name'>
            /// </param>
            public static bool? ApiAccountChangeNamePut(this IWebAPIClient operations, string token = default(string), string name = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountChangeNamePutAsync(token, name), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='token'>
            /// </param>
            /// <param name='name'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountChangeNamePutAsync(this IWebAPIClient operations, string token = default(string), string name = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountChangeNamePutWithHttpMessagesAsync(token, name, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='name'>
            /// </param>
            public static bool? ApiAccountChangeUserNamePut(this IWebAPIClient operations, string id = default(string), string name = default(string))
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountChangeUserNamePutAsync(id, name), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='name'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<bool?> ApiAccountChangeUserNamePutAsync(this IWebAPIClient operations, string id = default(string), string name = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiAccountChangeUserNamePutWithHttpMessagesAsync(id, name, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static void ApiAccountLogOffGet(this IWebAPIClient operations)
            {
                Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiAccountLogOffGetAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiAccountLogOffGetAsync(this IWebAPIClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.ApiAccountLogOffGetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<ApplicationUser> ApiDatabaseInitGet(this IWebAPIClient operations)
            {
                return Task.Factory.StartNew(s => ((IWebAPIClient)s).ApiDatabaseInitGetAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<ApplicationUser>> ApiDatabaseInitGetAsync(this IWebAPIClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ApiDatabaseInitGetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
