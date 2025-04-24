import {Component, OnInit} from '@angular/core';
import {BreadcrumbService} from "../../shared/breadcrumb/breadcrumb.service";
import {Router} from "@angular/router";
import {DialogService} from "../../shared/dialog/dialog.service";
import {CustomValidator, FormField, FormFieldTypes} from "../../shared/forms/form.interfaces";
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {FormGroup} from '@angular/forms';
import {AnyFormModule} from '../../shared/forms/any-form/any-form.module';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
import {SkladyPolozkyDataService} from './data/sklady-polozky-data.service';
import {Res} from './data/sklady-polozky.interfaces';
import {SharedModule} from '../../shared/shared.module';
@Component({
  selector: 'app-sklady-polozky',
  imports: [SharedModule],
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
        defaultValueGetter: () => {},
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
        defaultValueGetter: () => {},
        label: "Password",
        defaultValue: "password",
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
        defaultValueGetter: () => {},
        label: "Select me",
        defaultValue: "Second",
        formControlName: "Select",
        type: FormFieldTypes.LOOKUP,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [{displayValue: "First", value: "TEST"}, {displayValue: "Second", value: "Second"}]
      },
      {
        defaultValueGetter: () => {},
        label: "Select me 2",
        formControlName: "Select2",
        defaultValue: "First",
        type: FormFieldTypes.LOOKUP,
        options: [{displayValue: "First", value: "First"}, {displayValue: "Second", value: "Second"}]
      },
      {
        defaultValueGetter: () => {},
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
        defaultValueGetter: () => {},
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
        defaultValueGetter: () => {},
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
        defaultValueGetter: () => {},
        label: "tEXTAREA",
        formControlName: "tEXTAREA",
        type: FormFieldTypes.TEXTAREA,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
      },
      {
        defaultValueGetter: () => {},
        label: "Autocomplete classic",
        formControlName: "autocomplete",
        defaultValue: ["Second"],
        type: FormFieldTypes.AUTOCOMPLETE,
        multipleSelect: true,
        validator: [
          {
            validator: CustomValidator.REQUIRED
          },],
        options: [{displayValue: "First", value: "First"}, {displayValue: "Second", value: "Second"}]
      },
      {
        defaultValueGetter: () => {},
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

    const tes = await this.dialogService.form<Res>({fields: this.testingFields, title: "Test"})
    console.log(tes)

    /*const form: FormGroup = this.formService.createForm([{ name: 'firstName', label: 'First Name', type: 'text' },
      { name: 'lastName', label: 'Last Name', type: 'text' }])
   await this.router.navigate([this.router.url, 'nastaveni']);

    });*/

  }
}
