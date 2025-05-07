
export interface InvoiceItemCreateManyDto {
  invoiceId: string;
  items: InvoiceItemDto[];
  itemsForDelete: string[];
  deleteMissingItems: boolean;
}

export interface InvoiceItemDto {
  id?: string;
  containerItemId?: string,
  description: string;
  quantity: number;
  unitPrice: number;
  netAmount: number;
  vatRate: number;
  vatAmount: number;
  grossAmount: number;
}
