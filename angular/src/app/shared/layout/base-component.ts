import {Directive, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {TableSettings} from '../table/table/table.settings';
import {ToolbarButton} from '../context-toolbar/context-toolbar.interfaces';
import {DialogService} from '../dialog/dialog.service';
import {BreadcrumbService} from '../breadcrumb/breadcrumb.service';

export interface RouteData {
  basePath: string,
  breadcrumb: string
}
@Directive()
export abstract class BaseContentComponent<T, DataService> implements OnInit {
  tableSettings: TableSettings<T>| undefined;
  basePath: string | undefined;
  breadCrumbTitle: string | undefined;
  private _chosenElement?: T;
  entityId: string | null = "";
  toolbarButtons!: ToolbarButton[];

  protected constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected breadcrumbService: BreadcrumbService,
    protected dialogService: DialogService,
    protected dataService: DataService,
  ) {
    if(route.snapshot.data) {
      const data: Partial<RouteData> = this.route.snapshot.data as Partial<RouteData>;

      this.basePath = data.basePath;
      this.breadCrumbTitle = data.breadcrumb;
    }
  }

  ngOnInit() {
    this.route.paramMap.subscribe(pm => {
      if(pm.get('id')) {
        this.entityId = pm.get('id');
      }
    });
    this.refreshToolbarButtons();
  }

  get chosenElement(): T | undefined {
    return this._chosenElement;
  }

  set chosenElement(value: T | undefined) {
    this._chosenElement = value;
    this.refreshToolbarButtons();
  }

  protected refreshToolbarButtons() {
    this.toolbarButtons = this.buildToolbarButtons();
  }

  protected abstract buildToolbarButtons(): ToolbarButton<T>[];
}
