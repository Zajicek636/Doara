using System.Collections.Generic;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemManageReportDto
{
    public List<string> Errors { get; set; } = [];
    public bool HasErrors => Errors.Count != 0;
}