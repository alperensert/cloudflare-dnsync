using System.Linq.Expressions;
using CloudflareDnsync.Models;

namespace CloudflareDnsync.Services;

public interface IDnsyncConfigService
{
    List<DnsyncConfiguration> Get(Expression<Func<DnsyncConfiguration, bool>>? predicate = null);

    IEnumerable<DnsyncConfiguration> GetAll();

    DnsyncConfiguration? GetById(string id);

    DnsyncConfiguration? GetByName(string name);

    Task AddAsync(DnsyncConfiguration configuration, CancellationToken cancellationToken = default);

    Task RemoveAsync(DnsyncConfiguration configuration, CancellationToken cancellationToken = default);

    Task RemoveByNameAsync(string name, CancellationToken cancellationToken = default);

    Task UpdateAsync(DnsyncConfiguration configuration, CancellationToken cancellationToken = default);
}
