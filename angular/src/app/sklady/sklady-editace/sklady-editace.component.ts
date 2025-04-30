import {Component, OnInit} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {ColumnSetting, TableSettings} from '../../shared/table/table/table.settings';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {SkladyEditaceDataService} from './data/sklady-editace-data.service';
import {SkladyEditaceDto} from './data/sklady-editace.interfaces';
;

@Component({
  selector: 'app-sklady-editace',
  imports: [SharedModule],
  templateUrl: './sklady-editace.component.html',
  styleUrl: './sklady-editace.component.scss'
})
export class SkladyEditaceComponent implements OnInit {
  tableSettings!: TableSettings<SkladyEditaceDto>;

  constructor(
    protected dataService: SkladyEditaceDataService,
  ) {}

  ngOnInit() {

  }

  public handleDoubleClick(event: SkladyEditaceDto) {
    console.log(event)
  }

  loadData(): void {
  }

}
