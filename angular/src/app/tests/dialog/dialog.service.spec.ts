import { TestBed } from '@angular/core/testing';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { of, Subject } from 'rxjs';
import { FormGroup } from '@angular/forms';
import {DialogService} from '../../shared/dialog/dialog.service';
import {AlertDialogComponent} from '../../shared/dialog/components/alert-dialog.component';
import {ConfirmDialogComponent} from '../../shared/dialog/components/confirm-dialog.component';
import {DialogType} from '../../shared/dialog/dialog.interfaces';

describe('DialogService', () => {
  let service: DialogService;
  let dialogSpy: jasmine.SpyObj<MatDialog>;
  let mockDialogRef: jasmine.SpyObj<MatDialogRef<any>>;

  beforeEach(() => {
    mockDialogRef = jasmine.createSpyObj('MatDialogRef', ['afterClosed']);
    mockDialogRef.afterClosed.and.returnValue(of(true));

    dialogSpy = jasmine.createSpyObj('MatDialog', ['open']);
    dialogSpy.open.and.returnValue(mockDialogRef);

    TestBed.configureTestingModule({
      providers: [
        DialogService,
        { provide: MatDialog, useValue: dialogSpy }
      ]
    });

    service = TestBed.inject(DialogService);
  });

  it('should open any component via open()', async () => {
    const result = await service.open(AlertDialogComponent, { message: 'Hello' });
    expect(dialogSpy.open).toHaveBeenCalledWith(AlertDialogComponent, jasmine.any(Object));
    expect(result).toBeTrue();
  });

  it('should open alert dialog with correct data', async () => {
    await service.alert({
      title: 'Alert!',
      message: 'Something happened',
      dialogType: DialogType.WARNING
    });

    expect(dialogSpy.open).toHaveBeenCalledWith(AlertDialogComponent, jasmine.objectContaining({
      data: jasmine.objectContaining({
        title: 'Alert!',
        message: 'Something happened',
        type: DialogType.WARNING
      }),
      width: jasmine.any(String)
    }));
  });

  it('should open confirm dialog and return result', async () => {
    const result = await service.confirmAsync({
      message: 'Are you sure?',
      icon: 'help',
      dialogType: DialogType.ERROR
    });

    expect(dialogSpy.open).toHaveBeenCalledWith(ConfirmDialogComponent, jasmine.any(Object));
    expect(result).toBeTrue();
  });

  it('should open form dialog and subscribe to formReady', async () => {
    const formReadySubject = new Subject<{ sectionId: string, form: FormGroup }>();
    const mockInstance = {
      formReady: formReadySubject.asObservable()
    };

    const mockResult = {
      foo: {
        valid: true,
        data: {},
        modified: false,
        form: new FormGroup({})
      }
    };

    (dialogSpy.open as jasmine.Spy).and.returnValue({
      componentInstance: mockInstance,
      afterClosed: () => of(mockResult)
    } as any);

    const onFormReady = jasmine.createSpy('onFormReady');

    const result = await service.form({
      title: 'Form Title',
      sections: [],
      headerIcon: 'form',
      type: DialogType.SUCCESS
    }, onFormReady);

    // Simuluj emit formReady
    const testForm = new FormGroup({});
    formReadySubject.next({ sectionId: 'foo', form: testForm });

    expect(onFormReady).toHaveBeenCalledWith('foo', testForm);
    expect(result['foo'].valid).toBeTrue();
    expect(result['foo'].form).toEqual(jasmine.any(FormGroup));
    expect(result).toEqual({
      foo: {
        valid: true,
        data: {},
        modified: false,
        form: jasmine.any(FormGroup)
      }
    });
  });
});
