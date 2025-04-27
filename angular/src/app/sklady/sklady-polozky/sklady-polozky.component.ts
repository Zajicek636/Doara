import {Component, OnInit} from '@angular/core';
import {BreadcrumbService} from "../../shared/breadcrumb/breadcrumb.service";
import {Router} from "@angular/router";
import {DialogService} from "../../shared/dialog/dialog.service";
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {FormGroup} from '@angular/forms';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
import {SkladyPolozkyDataService} from './data/sklady-polozky-data.service';
import {SharedModule} from '../../shared/shared.module';
import {SUBJEKT_BASE_FIELDS} from '../../ucetnictvi/subjekty/data/subjekty.interfaces';
import {ContainerDto} from './data/sklady-polozky.interfaces';
@Component({
  selector: 'app-sklady-polozky',
  imports: [SharedModule],
  templateUrl: './sklady-polozky.component.html',
  styleUrl: './sklady-polozky.component.scss'
})
export class SkladyPolozkyComponent implements OnInit {
  items!: ContainerDto[];
  form: FormGroup;
  constructor(
      private dataService: SkladyPolozkyDataService,
      private breadcrumbService: BreadcrumbService,
      private router: Router,
      private dialogService: DialogService,
  ) {
    this.form = new FormGroup({});
  }

  ngOnInit() {
    this.loadData()
  }

  public handleChange(res: FormComponentResult<ContainerDto>) {
    this.form = res.form
    console.log(res.data);
  }


  async loadData() {
    try {
      //await this.dataService.get("test")
      this.items = [{id: "1", name: "Container1"},{id: "2", name: "Container2"},{id: "3", name: "Container3"}]
    } catch (e) {
      await this.dialogService.alert({title: "Titulek", message:`${e}`, dialogType: DialogType.WARNING})
    }
  }

  async route() {
  }

}
