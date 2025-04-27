import {Component, OnInit} from '@angular/core';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {SubjektyDataService} from '../subjekty/data/subjekty-data.service';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {SharedModule} from '../../shared/shared.module';

@Component({
  selector: 'app-nova-faktura',
  imports: [SharedModule],
  templateUrl: './nova-faktura.component.html',
  styleUrl: './nova-faktura.component.scss'
})
export class NovaFakturaComponent extends BaseContentComponent<any,any> implements OnInit{
  constructor(
    protected override dataService: SubjektyDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute) {
    super(route, router,breadcrumbService, dialogService, dataService);
  }
  override ngOnInit() {
    super.ngOnInit();
    this.createForm()
  }

  async createForm() {

  }

  protected buildToolbarButtons(): ToolbarButton[] {
    return [
      {
        id: 'saveFaktura',
        text: 'Uložit',
        icon: BaseMaterialIcons.SAVE,
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => {}
      },

      {
        id: 'cancelFaktura',
        text: 'Zrušit',
        icon: BaseMaterialIcons.CANCEL,
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        visible: true,
        action: () => {}
      },
      {
        id: 'refresh',
        text: 'Aktualizovat',
        icon: BaseMaterialIcons.REFRESH,
        class: 'btn-danger',
        disabled: !this.chosenElement,
        visible: true,
        action: () => {}
      }
    ];
  }



}
