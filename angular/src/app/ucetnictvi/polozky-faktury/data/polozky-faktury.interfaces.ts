export interface InvoiceItemCreateDto {
  id: string;
  description: string;
  quantity: number;
  unitPrice: number;
  vatRate: number;
  netAmount: number;
  vatAmount: number;
  grossAmount: number;
}

export interface InvoiceItemCreateManyDto {
  invoiceId: string;
  items: InvoiceItemCreateDto[];
  itemsForDelete: string[];
  deleteMissingItems: boolean;
}

export interface InvoiceItemDto {
  id: string;
  invoiceId: string;
  description: string;
  quantity: number;
  unitPrice: number;
  netAmount: number;
  vatRate: number;
  vatAmount: number;
  grossAmount: number;
}
