import {PolozkyFakturyDataService} from '../../ucetnictvi/polozky-faktury/data/polozky-faktury-data.service';
import {PolozkyFakturyCrudSettings} from '../../ucetnictvi/polozky-faktury/data/polozky-faktury-crud.settings';
import {HttpClient} from '@angular/common/http';
import {of} from 'rxjs';

describe('PolozkyFakturyDataService', () => {
  let service: PolozkyFakturyDataService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['post']);
    const settings = new PolozkyFakturyCrudSettings();
    service = new PolozkyFakturyDataService(httpClientSpy, settings);
  });

  it('should call POST with ManageMany suffix', async () => {
    const dto = {
      invoiceId: '789',
      items: [],
      itemsForDelete: []
    };
    httpClientSpy.post.and.returnValue(of({}));

    await service.post('', dto, { useSuffix: true });

    expect(httpClientSpy.post).toHaveBeenCalledWith(
      jasmine.stringMatching(/\/api\/Ucetnictvi\/InvoiceItem\/ManageMany$/),
      jasmine.objectContaining({
        invoiceId: '789',
        items: [],
        itemsForDelete: []
      })
    );
  });
});
