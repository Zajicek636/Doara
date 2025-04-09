import {Component, OnInit} from '@angular/core';
import {SkladyPolozkyDataService} from './sklady-polozky-data.service';
import {BreadcrumbService} from "../../shared/breadcrumb/breadcrumb.service";
import {Router} from "@angular/router";
import {MatButton} from "@angular/material/button";
import {DialogService} from "../../shared/dialog/dialog.service";

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
  constructor(private dataService: SkladyPolozkyDataService, private breadcrumbService: BreadcrumbService, private router: Router, private dialogService: DialogService) {
  }

  ngOnInit() {
    this.loadData()
  }

  async loadData() {
    try {
      await this.dataService.get("test")
    } catch (e) {
      await this.dialogService.confirmAsync("test","test")
      //await this.dialogService.alert({title: "Titulek", message:`${e}`, dialogType: DialogType.SUCCESS})
    }

  }

  route() {
   this.router.navigate([this.router.url, 'nastaveni']);
  }
}
