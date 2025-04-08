import {Component, OnInit} from '@angular/core';
import {SkladyPolozkyDataService} from './sklady-polozky-data.service';

@Component({
  selector: 'app-sklady-polozky',
  imports: [],
  templateUrl: './sklady-polozky.component.html',
  styleUrl: './sklady-polozky.component.scss'
})
export class SkladyPolozkyComponent implements OnInit {
  constructor(private dataService: SkladyPolozkyDataService) {
  }

  ngOnInit() {
   this.loadData()
  }

  async loadData() {
    await this.dataService.get("tes")
  }
}
