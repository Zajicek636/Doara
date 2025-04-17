using System;
using Doara.Ucetnictvi.Constants;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class Address : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; }
    public virtual Guid? TenantId { get; }
    public virtual string Street { get; private set; }
    public virtual string City { get; private set; }
    public virtual string PostalCode { get; private set; }
    public virtual Guid CountryId { get; private set; }
    public virtual Country Country { get; }

    public Address(Guid id, string street, string city, string postalCode, Guid countryId) : base(id)
    {
        Street = Check.NotNullOrWhiteSpace(street, nameof(Street), AddressConstants.MaxStreetLength);
        City = Check.NotNullOrWhiteSpace(city, nameof(City), AddressConstants.MaxCityLength);
        PostalCode = Check.NotNullOrWhiteSpace(postalCode, nameof(PostalCode), AddressConstants.MaxPostalCodeLength);
        CountryId = Check.NotNull(countryId, nameof(CountryId));
    }
}