import { Injectable } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, PRIMARY_OUTLET } from '@angular/router';
import { filter } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

export interface IBreadCrumb {
  label: string;
  url: string;
}

@Injectable({ providedIn: 'root' })
export class BreadcrumbService {
  private _breadcrumbs = new BehaviorSubject<IBreadCrumb[]>([]);
  readonly breadcrumbs$ = this._breadcrumbs.asObservable();

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(() => {
        // 1) Vezmi dříve uložené crumby (pokud existují)
        const history: IBreadCrumb[] = window.history.state.previousBreadcrumbs ?? [];

        // 2) Vygeneruj nové podle URL & data.breadcrumb
        const generated = this.buildBreadcrumbs(this.activatedRoute.root);

        // 3) Sloučíme obojí
        this._breadcrumbs.next([ ...history, ...generated ]);
      });
  }

  public get breadcrumbsValue(): IBreadCrumb[] {
    return this._breadcrumbs.getValue();
  }

  private buildBreadcrumbs(
    route: ActivatedRoute,
    url: string = '',
    crumbs: IBreadCrumb[] = []
  ): IBreadCrumb[] {
    const children = route.children.filter(r => r.outlet === PRIMARY_OUTLET);
    if (!children.length) {
      return crumbs;
    }

    for (const child of children) {
      const segment = child.snapshot.url.map(u => u.path).join('/');
      if (segment) {
        url += `/${segment}`;
      }
      const label = child.snapshot.data['breadcrumb'];
      if (label) {
        crumbs.push({ label, url });
      }
      return this.buildBreadcrumbs(child, url, crumbs);
    }
    return crumbs;
  }
}
