import {Component, OnInit} from '@angular/core';
import {SkladyPolozkyDataService} from './sklady-polozky-data.service';
import {BreadcrumbService} from "../../shared/breadcrumb/breadcrumb.service";
import {Router} from "@angular/router";
import {DialogService} from "../../shared/dialog/dialog.service";
import {CustomValidator, FormField, FormFieldTypes} from "../../shared/forms/form.interfaces";
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {FormGroup} from '@angular/forms';
import {AnyFormModule} from '../../shared/forms/any-form/any-form.module';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
export interface Res {
  Test: string,
  Cislo: string,
}
@Component({
  selector: 'app-sklady-polozky',
  imports: [AnyFormModule],
  templateUrl: './sklady-polozky.component.html',
  styleUrl: './sklady-polozky.component.scss'
})
export class SkladyPolozkyComponent implements OnInit {
  testingFields:FormField[];
  form: FormGroup;
  constructor(
      private dataService: SkladyPolozkyDataService,
      private breadcrumbService: BreadcrumbService,
      private router: Router,
      private dialogService: DialogService,
  ) {
    this.form = new FormGroup({});

    this.testingFields  = [
      {
        label: "Test",
        defaultValue: "TEST",
        formControlName: "Test",
        type: FormFieldTypes.TEXT,
        validator: [{
          validator: CustomValidator.REQUIRED
        },{
          validator: CustomValidator.MAX,
          params: 10
        },{
          validator: CustomValidator.MIN,
          params: 3
        }]
      }, {
        label: "Password",
        formControlName: "Cislo",
        type: FormFieldTypes.PASSWORD,
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
      },
      {
        label: "Select me",
        formControlName: "Select",
        type: FormFieldTypes.LOOKUP,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [{displayValue: "First", value: "TEST"}, {displayValue: "Second", value: "Second"}]
      },
      {
        label: "Select me 2",
        formControlName: "Select2",
        type: FormFieldTypes.LOOKUP,
        options: [{displayValue: "First", value: "First"}, {displayValue: "Second", value: "Second"}]
      },
      {
        label: "Multiple select",
        formControlName: "Select3",
        multipleSelect: true,
        type: FormFieldTypes.LOOKUP,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [{displayValue: "First", value: "First"}, {displayValue: "Second", value: "Second"}]
      },
      {
        label: "Grouped select",
        formControlName: "Select4",
        type: FormFieldTypes.LOOKUP,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [
          {groupName: "TEST GROUP", val:[{displayValue: "First", value: "First"}]},
          {groupName: "SECOND GROUP", val:[{displayValue: "Second", value: "Second"}]}
        ]
      },
      {
        label: "Grouped multiple select",
        formControlName: "Select5",
        multipleSelect: true,
        type: FormFieldTypes.LOOKUP,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [
          {groupName: "TEST GROUP", val:[{displayValue: "First", value: "TEST"}]},
          {groupName: "SECOND GROUP", val:[{displayValue: "Second", value: "TEST"}]}
        ]
      },
      {
        label: "tEXTAREA",
        formControlName: "tEXTAREA",
        type: FormFieldTypes.TEXTAREA,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
      },
      {
        label: "Autocomplete classic",
        formControlName: "autocomplete",
        type: FormFieldTypes.AUTOCOMPLETE,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [{displayValue: "First", value: "First"}, {displayValue: "Second", value: "Second"}]
      },
      {
        label: "Autocomplete grouped",
        formControlName: "autocomplete2",
        type: FormFieldTypes.AUTOCOMPLETE,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [
          {groupName: "TEST GROUP", val:[{displayValue: "First", value: "TEST"}]},
          {groupName: "SECOND GROUP", val:[{displayValue: "Second", value: "TEST"}]}
        ]
      },
    ]
  }

  ngOnInit() {
    this.loadData()
  }

  public handleChange(res: FormComponentResult<Res>) {
    this.form = res.form
    console.log(res.data);
  }


  async loadData() {
    try {
      await this.dataService.get("test")
    } catch (e) {
      await this.dialogService.alert({title: "Titulek", message:`${e}`, dialogType: DialogType.WARNING})
    }
  }

  async route() {

    const tes = await this.dialogService.form<Res>({fields: this.testingFields})
    console.log(tes)

    /*const form: FormGroup = this.formService.createForm([{ name: 'firstName', label: 'First Name', type: 'text' },
      { name: 'lastName', label: 'Last Name', type: 'text' }])
   await this.router.navigate([this.router.url, 'nastaveni']);

    });*/

  }
}
