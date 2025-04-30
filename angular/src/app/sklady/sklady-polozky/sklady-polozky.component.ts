import {Component, OnInit} from '@angular/core';
import {BreadcrumbService, IBreadCrumb} from "../../shared/breadcrumb/breadcrumb.service";
import {ActivatedRoute, Router} from "@angular/router";
import {DialogService} from "../../shared/dialog/dialog.service";

import {FormGroup} from '@angular/forms';
import {DialogType, DynamicDialogResult} from '../../shared/dialog/dialog.interfaces';
import {SkladyPolozkyDataService} from './data/sklady-polozky-data.service';
import {SharedModule} from '../../shared/shared.module';
import {ContainerDto, CREATE_CONTAINER_FIELDS} from './data/sklady-polozky.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {FormField} from '../../shared/forms/form.interfaces';
import {populateDefaults} from '../../shared/forms/form-field.utils';
import {PagedList} from '../../shared/crud/base-crud-service';

@Component({
  selector: 'app-sklady-polozky',
  imports: [SharedModule],
  templateUrl: './sklady-polozky.component.html',
  styleUrl: './sklady-polozky.component.scss'
})
export class SkladyPolozkyComponent extends BaseContentComponent<ContainerDto,SkladyPolozkyDataService> implements OnInit {
  items: ContainerDto[] = [];
  form: FormGroup;
  containerFields!: FormField[];

  constructor(
    protected override dataService: SkladyPolozkyDataService,
    protected override  breadcrumbService: BreadcrumbService,
    protected override  router: Router,
    protected override route: ActivatedRoute,
    protected override  dialogService: DialogService,
    ) {
    super(route, router,breadcrumbService, dialogService, dataService);
    this.form = new FormGroup({});
  }

  override ngOnInit() {
    super.ngOnInit();
    this.loadData()
  }

  async loadData() {
    try {
      //const res = await this.dataService.getAll({useSuffix: true})
      //this.items = res.items ?? []
      for (let i = 0; i < 10; i++) {
        this.items.push({id: `${i}`, name: `TEST-${i}`, description: `DESCRIPTIONDESCRIPTION-${i}`});
      }
    } catch (e) {
      await this.dialogService.alert({title: "Titulek", message:`${e}`, dialogType: DialogType.WARNING})
    }
  }



  protected buildToolbarButtons(): ToolbarButton<ContainerDto>[] {
    return [
      {
        id: 'addContainer',
        text: 'Přidat kontejner',
        icon: BaseMaterialIcons.ADD_CONTAINER,
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => this.addNewContainer()
      },
    ];
  }

  async handleEditClick(item: ContainerDto) {
    this.containerFields = populateDefaults(CREATE_CONTAINER_FIELDS, item)

    const a = await this.dialogService.form({
      headerIcon: BaseMaterialIcons.EDIT_PERSON,
      title: `Editace kontejneru - ${item.name}`,
      sections: [
        {
          sectionId: "main_section",
          headerIcon: BaseMaterialIcons.ASSIGNMENT,
          fields: this.containerFields,
          sectionTitle: "Základní informace"
        }],
      type: DialogType.SUCCESS
    })
    const res = this.mapToDto(a,"main_section")
    await this.dataService.put(res.id, res)
    this.items = this.items.map(x => x.id == res.id ? res : x)
  }

  async handleDeleteClick(item: ContainerDto) {
    await this.dialogService.confirmAsync({
      title: "Potvrzení operace",
      message: `Opravdu chcete odebrat kontejner ${item.name} a jeho položky?`,
      dialogType: DialogType.ALERT,
    })
    await this.dataService.delete(item.id);
    this.items.splice(this.items.indexOf(item), 1);
  }

  async handleClickContainer(item: ContainerDto) {
    const prev: IBreadCrumb[] = this.breadcrumbService.breadcrumbsValue;
    await this.router.navigate(['polozky', item.id], { relativeTo: this.route.parent, state: { previousBreadcrumbs: prev } });
  }

  async addNewContainer() {
    this.containerFields = CREATE_CONTAINER_FIELDS;

    const a = await this.dialogService.form({
      headerIcon: BaseMaterialIcons.ADD_CONTAINER,
      title: "Nový kontejner",
      sections: [
        {
          sectionId: "main_section",
          headerIcon: BaseMaterialIcons.ASSIGNMENT,
          fields: this.containerFields,
          sectionTitle: "Základní informace"
        }],
      type: DialogType.SUCCESS
    })
    if(!a) return;

    const obj = this.mapToDto(a,"main_section")
    const res = await this.dataService.post('',obj)
    this.items.push(res)
  }

  mapToDto(result: DynamicDialogResult, key: string): ContainerDto {
    return {
      name: result[key].data.ContainerName,
      id: result[key].data.ContainerId ?? '',
      description: result[key].data.ContainerLabel
    }
  }

  protected readonly BaseMaterialIcons = BaseMaterialIcons;
}
