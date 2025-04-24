import {Component, OnInit} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {ColumnSetting, TableSettings} from '../../shared/table/table/table.settings';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {SkladyEditaceDataService} from './data/sklady-editace-data.service';
import {SeznamFakturDto} from '../../ucetnictvi/seznam-faktur/data/seznam-faktur.interfaces';
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
    private breadcrumbService: BreadcrumbService,
    private router: Router,
    private dialogService: DialogService,
  ) {

  }

  ngOnInit() {
    const columnSettings: ColumnSetting<SkladyEditaceDto>[] = [
      { key: 'id',header: 'Jméno', valueGetter: row => row.id },
    ]

  /*  this.tableSettings = {
      cacheEntityType: "entity",
      displayedColumns: columnSettings,
      clickable: true,
      expandable: false,
      pageSizeOptions: [5, 10, 25, 100],
      defaultPageSize: 10,
      extraQueryParams: { active: true }
    };*/
  }

  public handleDoubleClick(event: SkladyEditaceDto) {
    console.log(event)
  }

  loadData(): void {
  }

}
