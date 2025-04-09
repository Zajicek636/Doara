import {Component, OnInit} from '@angular/core';
import {SkladyPolozkyDataService} from './sklady-polozky-data.service';
import {BreadcrumbService} from "../../shared/breadcrumb/breadcrumb.service";
import {Router} from "@angular/router";
import {MatButton} from "@angular/material/button";
import {DialogService} from "../../shared/dialog/dialog.service";
import {DialogType} from "../../shared/dialog/dialog.interfaces";
import {FormService} from "../../shared/forms/form.service";
import {ValidatorService} from "../../shared/forms/validator.service";
import {CustomValidator, FormField, FormFieldTypes} from "../../shared/forms/form.interfaces";

@Component({
  selector: 'app-sklady-polozky',
  imports: [
    MatButton
  ],
  templateUrl: './sklady-polozky.component.html',
  styleUrl: './sklady-polozky.component.scss'
})
export class SkladyPolozkyComponent implements OnInit {
  breadcrumbs: Array<{label: string, url: string}> = [];
  constructor(
      private dataService: SkladyPolozkyDataService,
      private breadcrumbService: BreadcrumbService,
      private router: Router,
      private dialogService: DialogService,
      private formService: FormService,
  ) {
  }

  ngOnInit() {
    this.loadData()
  }

  async loadData() {
    try {
      await this.dataService.get("test")
    } catch (e) {
      //await this.dialogService.alert({title: "Titulek", message:`${e}`, dialogType: DialogType.ERROR})
    }

  }

  async route() {
    const a: FormField[] = [{
      label: "Test",
      defaultValue: "TEST",
      formControlName: "Test",
      type: FormFieldTypes.TEXT,
      validator: [{
        validator: CustomValidator.REQUIRED
      }]
    },
      {
        label: "Cislo",
        formControlName: "Cislo",
        type: FormFieldTypes.NUMBER,
        validator: [
            {
              validator: CustomValidator.REQUIRED
            },
          {
            validator: CustomValidator.MIN,
            params: 5
          },
          {
            validator: CustomValidator.MAX,
            params: 10
          }]
      }]

    await this.dialogService.form({fields: a})

    /*const form: FormGroup = this.formService.createForm([{ name: 'firstName', label: 'First Name', type: 'text' },
      { name: 'lastName', label: 'Last Name', type: 'text' }])
   await this.router.navigate([this.router.url, 'nastaveni']);

    });*/

  }
}
