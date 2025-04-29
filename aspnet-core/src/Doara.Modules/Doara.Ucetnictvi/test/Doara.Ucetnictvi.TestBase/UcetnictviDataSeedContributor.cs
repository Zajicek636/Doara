using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.Repositories;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi;

public class UcetnictviDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly ICountryRepository _countryRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceItemRepository _invoiceItemRepository;

    public UcetnictviDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant, ICountryRepository countryRepository, 
        IAddressRepository addressRepository, ISubjectRepository subjectRepository, 
        IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _countryRepository = countryRepository;
        _addressRepository = addressRepository;
        _subjectRepository = subjectRepository;
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedCountryAsync();
            await SeedAddressAsync();
            await SeedSubjectAsync();
            await SeedInvoiceAsync();
            await SeedInvoiceItemAsync();
        }
    }

    private async Task SeedCountryAsync()
    {
        await _countryRepository.CreateAsync(new Country(TestData.CzCountryId, "Česká republika", "CZ"));
        await _countryRepository.CreateAsync(new Country(TestData.SkCountryId, "Slovensko", "SK"));
        await _countryRepository.CreateAsync(new Country(TestData.UsCountryId, "United States", "USA"));
    }
    
    private async Task SeedAddressAsync()
    {
        await _addressRepository.CreateAsync(new Address(
            TestData.CzAddressId, 
            "Václavské náměstí 1", 
            "Praha", 
            "11000", 
            TestData.CzCountryId
        ));

        await _addressRepository.CreateAsync(new Address(
            TestData.SkAddressId,
            "Hlavná 5", 
            "Bratislava", 
            "81101", 
            TestData.SkCountryId
        ));

        await _addressRepository.CreateAsync(new Address(
            TestData.UsAddressId,
            "1600 Pennsylvania Avenue NW", 
            "Washington", 
            "20500", 
            TestData.UsCountryId
        ));
    }

    private async Task SeedSubjectAsync()
    {
        await _subjectRepository.CreateAsync(new Subject(
            TestData.CzSubjectId,
            "Alza.cz a.s.",
            TestData.CzAddressId,
            "27082440",
            "CZ27082440",
            true
        ));

        await _subjectRepository.CreateAsync(new Subject(
            TestData.SkSubjectId,
            "Martinus, s.r.o.",
            TestData.SkAddressId,
            "35840773",
            "SK2020269786",
            true
        ));

        await _subjectRepository.CreateAsync(new Subject(
            TestData.UsSubjectId,
            "Amazon Inc.",
            TestData.UsAddressId,
            null,
            "US-94-32821",
            false
        ));
    }
    
    private async Task SeedInvoiceAsync()
    {
        await _invoiceRepository.CreateAsync(new Invoice(
                TestData.CzInvoiceId,
                "2025-1001",
                TestData.CzSubjectId,
                TestData.SkSubjectId,
                new DateTime(2025, 4, 10),
                new DateTime(2025, 4, 10),
                new DateTime(2025, 4, 9),
                10000m,
                2100m,
                12100m,
                "Splatnost do 14 dnů",
                VatRate.Standard21,
                "10012025",
                "0308",
                "0555"
            )
        );

        await _invoiceRepository.CreateAsync(new Invoice(
            TestData.SkInvoiceId1,
            "2025-2001",
            TestData.SkSubjectId,
            TestData.CzSubjectId,
            new DateTime(2025, 3, 25),
            new DateTime(2025, 3, 25),
            new DateTime(2025, 3, 24),
            5000m,
            1050m,
            6050m,
            "Do 7 dní",
            VatRate.Reduced12,
            "20012503",
            "0558",
            "8888"
        ));

        await _invoiceRepository.CreateAsync(new Invoice(
            TestData.SkInvoiceId2,
            "2025-2002",
            TestData.SkSubjectId,
            TestData.CzSubjectId,
            new DateTime(2025, 4, 1),
            null,
            null,
            7000m,
            1470m,
            8470m,
            "Splatnost 30 dní",
            VatRate.Standard21,
            "20022504",
            null,
            null
        ));

        await _invoiceRepository.CreateAsync(new Invoice(
            TestData.SkInvoiceId3,
            "2025-2003",
            TestData.SkSubjectId,
            TestData.CzSubjectId,
            new DateTime(2025, 2, 15),
            new DateTime(2025, 2, 16),
            new DateTime(2025, 2, 14),
            1500m,
            315m,
            1815m,
            "Hotově",
            VatRate.Reduced12,
            "20031502",
            "0309",
            "1234"
        ));
    }
    
    private async Task SeedInvoiceItemAsync()
    {
        await _invoiceItemRepository.CreateAsync(new InvoiceItem(
            TestData.CzInvoiceItemId, 
            TestData.CzInvoiceId,
            "Dodávka IT služeb",
            1,
            10000m,
            10000m,
            VatRate.Standard21,
            2100m,
            12100m
            ));

        await _invoiceItemRepository.CreateAsync(new InvoiceItem(
            TestData.SkInvoiceItemId11,
            TestData.SkInvoiceId1,
            "Konzultačné služby",
            1,
            1500m,
            1500m,
            VatRate.Reduced12,
            315m,
            1815m
        ));

            
        await _invoiceItemRepository.CreateAsync(new InvoiceItem(
            TestData.SkInvoiceItemId31,
            TestData.SkInvoiceId3,
            "Vývoj webovej aplikácie",
            1,
            3000m,
            3000m,
            VatRate.Standard21,
            630m,
            3630m
        ));

        await _invoiceItemRepository.CreateAsync(new InvoiceItem(
            TestData.SkInvoiceItemId32,
            TestData.SkInvoiceId3,
            "Testing a QA",
            2,
            1000m,
            2000m,
            VatRate.Standard21,
            420m,
            2420m
        ));

        await _invoiceItemRepository.CreateAsync(new InvoiceItem(
            TestData.SkInvoiceItemId33,
            TestData.SkInvoiceId3,
            "Dokumentácia",
            1,
            2000m,
            2000m,
            VatRate.Standard21,
            420m,
            2420m
        ));
    }
}
