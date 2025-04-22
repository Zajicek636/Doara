import {Directive} from '@angular/core';
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
export abstract class BaseContentComponent<T, DataService> {
  tableSettings: TableSettings | undefined;
  basePath: string | undefined;
  breadCrumbTitle: string | undefined;
  protected _chosenElement?: T;

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

  get toolbarButtons(): ToolbarButton[] {
    return [];
  }

  get chosenElement(): T | undefined {
    return this._chosenElement;
  }

  set chosenElement(value: T | undefined) {
    this._chosenElement = value;
  }

}
