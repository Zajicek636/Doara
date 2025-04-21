using Doara.Ucetnictvi.IAppServices;
using Volo.Abp.Guids;

namespace Doara.Ucetnictvi.Tests;

public class InvoiceItemAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly IInvoiceItemAppService _invoiceItemAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public InvoiceItemAppService_Tests()
    {
        _invoiceItemAppService = GetRequiredService<IInvoiceItemAppService>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
    }

}