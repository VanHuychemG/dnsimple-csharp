using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using static dnsimple.Services.Paths;

namespace dnsimple.Services
{
    /// <inheritdoc cref="DomainsService"/>
    /// <see>https://developer.dnsimple.com/v2/domains/pushes/</see>
    public partial class DomainsService
    {
        /// <summary>
        /// Initiates a pust of a domain to another DNSimple account.
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <param name="domainIdentifier">The domain name or ID</param>
        /// <param name="email">The email address of the target DNSimple account.</param>
        /// <returns>The newly created push.</returns>
        /// <see>https://developer.dnsimple.com/v2/domains/pushes/#initiateDomainPush</see>
        public SimpleResponse<Push> InitiatePush(long accountId, string domainIdentifier, string email)
        {
            if(string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty");
            
            var builder = BuildRequestForPath(InitiatePushPath(accountId, domainIdentifier));
            builder.Method(Method.POST);
            builder.AddJsonPayload(PushPayload("new_account_email", email));

            return new SimpleResponse<Push>(Execute(builder.Request));
        }

        /// <summary>
        /// List pending pushes for the target account.
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <returns>A list of the pending pushes.</returns>
        /// <see>https://developer.dnsimple.com/v2/domains/pushes/#listPushes</see>
        public PaginatedResponse<Push> ListPushes(long accountId)
        {
            var builder = BuildRequestForPath(PushPath(accountId)); 

            return new PaginatedResponse<Push>(Execute(builder.Request));
        }

        /// <summary>
        /// Accepts a push to the target account.
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <param name="pushId">The push id</param>
        /// <param name="contactId">A contact that belongs to the target
        /// DNSimple account. The contact will be used as new registrant for
        /// the domain, if the domain is registered with DNSimple.</param>
        /// <see>https://developer.dnsimple.com/v2/domains/pushes/#acceptPush</see>
        public EmptyResponse AcceptPush(long accountId, long pushId, long contactId)
        {
            var builder = BuildRequestForPath(PushPath(accountId, pushId));
            builder.Method(Method.POST);
            builder.AddJsonPayload(PushPayload("contact_id", contactId.ToString()));

            return new EmptyResponse(Execute(builder.Request));
        }

        /// <summary>
        /// Rejects a push to the target account.
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <param name="pushId">The push id</param>
        /// <see>https://developer.dnsimple.com/v2/domains/pushes/#rejectPush</see>
        public EmptyResponse RejectPush(int accountId, int pushId)
        {
            var builder = BuildRequestForPath(PushPath(accountId, pushId));
            builder.Method(Method.DELETE);

            return new EmptyResponse(Execute(builder.Request));
        }

        private static JsonObject PushPayload(string key, string value)
        {
            var payload = new JsonObject
            {
                new KeyValuePair<string, object>(key, value)
            };
            return payload;
        }
    }

    /// <summary>
    /// Represents a pending push.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public struct Push
    {
        public long Id { get; set; }
        public long DomainId { get; set; }
        public long? ContactId { get; set; }
        public long AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
