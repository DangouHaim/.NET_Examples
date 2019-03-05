﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace TicketManagement.WPF.WebAPI.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class UserInfo
    {
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        public UserInfo() { }

        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        public UserInfo(ApplicationUser user = default(ApplicationUser), string userId = default(string), IList<string> roles = default(IList<string>))
        {
            User = user;
            UserId = userId;
            Roles = roles;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public ApplicationUser User { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "roles")]
        public IList<string> Roles { get; set; }

    }
}
