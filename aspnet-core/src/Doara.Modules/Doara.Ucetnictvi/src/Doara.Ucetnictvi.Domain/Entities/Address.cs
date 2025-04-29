using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Doara.Ucetnictvi.Constants;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class Address : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; private set; }
    public virtual Guid? TenantId { get; private set; }
    public virtual string Street { get; private set; }
    public virtual string City { get; private set; }
    public virtual string PostalCode { get; private set; }
    public virtual Guid CountryId { get; private set; }
    public virtual Country Country { get; private set; }
    public virtual ICollection<Subject> Subjects { get; private set; }

    public Address(Guid id, string street, string city, string postalCode, Guid countryId) : base(id)
    {
        SetStreet(street)
            .SetCity(city)
            .SetPostalCode(postalCode)
            .SetCountry(countryId);
        Subjects = new Collection<Subject>();
    }

    public Address SetStreet(string street)
    {
        Street = Check.NotNullOrWhiteSpace(street, nameof(Street), AddressConstants.MaxStreetLength);
        return this;
    }
    
    public Address SetCity(string city)
    {
        City = Check.NotNullOrWhiteSpace(city, nameof(City), AddressConstants.MaxCityLength);
        return this;
    }
    
    public Address SetPostalCode(string postalCode)
    {
        PostalCode = Check.NotNullOrWhiteSpace(postalCode, nameof(PostalCode), AddressConstants.MaxPostalCodeLength);
        return this;
    }
    
    public Address SetCountry(Guid countryId)
    {
        CountryId = Check.NotNull(countryId, nameof(CountryId));
        return this;
    }
    
    public Address SetCountry(Country country)
    {
        Country = country;
        return SetCountry(country.Id);
    }
}