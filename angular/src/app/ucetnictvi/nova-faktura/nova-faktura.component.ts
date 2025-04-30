import {Component, OnInit} from '@angular/core';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {SubjektyDataService} from '../subjekty/data/subjekty-data.service';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {SharedModule} from '../../shared/shared.module';
import {NovaFakturaDataService} from './data/nova-faktura-data.service';
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {populateDefaults} from '../../shared/forms/form-field.utils';
import {CREATE_FAKTURA_FIELDS, InvoiceCreateDto} from './data/nova-faktura.interfaces';
import {SubjektDetailDto} from '../subjekty/data/subjekty.interfaces';
import {FormGroup} from '@angular/forms';
import {FormField} from '../../shared/forms/form.interfaces';
import {InvoiceDto} from '../seznam-faktur/data/seznam-faktur.interfaces';

@Component({
  selector: 'app-nova-faktura',
  imports: [SharedModule],
  templateUrl: './nova-faktura.component.html',
  styleUrl: './nova-faktura.component.scss'
})
export class NovaFakturaComponent extends BaseContentComponent<any,any> implements OnInit {
  formFields: FormField[] = [];
  defaultValues: any = {};
  isFormValid = false;
  isFormModified = false;
  isNew = true;
  entity: InvoiceDto | null = null;
  formGroup!: FormGroup;
  subjektOptions:any[] = [];
  loaded: boolean = false;

  constructor(
    private subjektyDataService: SubjektyDataService,
    protected override dataService: NovaFakturaDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute
  ) {
    super(route, router, breadcrumbService, dialogService, dataService);
  }

  override async ngOnInit() {
    super.ngOnInit();
    const id = this.route.snapshot.paramMap.get('id');
    this.isNew = !id;
    await this.loadSubjectsAndInitForm();
    this.loaded = true;
  }

  private async loadSubjectsAndInitForm() {
    const page = await this.subjektyDataService.getAll();
    const subjects = page.items;
    this.subjektOptions = subjects.map((s: SubjektDetailDto) => ({
      value: s.id,
      displayValue: `${s.name} – ${s.ic} – ${s.dic}`
    }));

    //options pro subjektField a supplierField
    this.formFields = CREATE_FAKTURA_FIELDS.map(f => {
      if (f.formControlName === 'supplierId' || f.formControlName === 'customerId') {
        return { ...f, options: this.subjektOptions };
      }
      return f;
    });

    this.formFields = populateDefaults(this.formFields, this.isNew ? {} as InvoiceCreateDto : this.entity);

  }

  override buildToolbarButtons(): ToolbarButton[] {
    return [
      {
        id: 'saveFaktura',
        text: 'Uložit',
        icon: BaseMaterialIcons.SAVE,
        class: 'btn-primary',
        visible: true,
        disabled: !this.isFormValid || !this.isFormModified,
        action: () => this.saveFaktura()
      },
      {
        id: 'cancelFaktura',
        text: 'Zrušit',
        icon: BaseMaterialIcons.CANCEL,
        class: 'btn-secondary',
        disabled: false,
        visible: true,
        action: () => this.router.navigate(['/seznam-faktur']),
      }
    ];
  }

  onFormChanged(result: FormComponentResult) {
    this.isFormValid = result.valid;
    this.isFormModified = result.modified;
    this.formGroup = result.form;

    // získáme nově vybrané id
    const data = result.data as InvoiceCreateDto;
    const selSupplier = data.supplierId;
    const selCustomer = data.customerId;

    // upravíme options tak, aby se nevyskytla vybraná hodnota
    this.formFields = this.formFields.map(f => {
      if (f.formControlName === 'supplierId') {
        return { ...f, options: this.subjektOptions.filter(o => o.value !== selCustomer) };
      }
      if (f.formControlName === 'customerId') {
        return { ...f, options: this.subjektOptions.filter(o => o.value !== selSupplier) };
      }
      return f;
    });
  }

  private saveFaktura() {
  }

}
