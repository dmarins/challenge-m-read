using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace M.Challenge.Read.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ApiKey
    {
        public int Id { get; }
        public string Owner { get; }
        public Guid Key { get; }
        public DateTime Created { get; }
        public IReadOnlyCollection<string> Roles { get; }

        public ApiKey(int id, string owner, Guid key, DateTime created, IReadOnlyCollection<string> roles)
        {
            Id = id;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Key = key == Guid.Empty ? throw new ArgumentException(nameof(key)) : key;
            Created = created;
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }
    }
}
