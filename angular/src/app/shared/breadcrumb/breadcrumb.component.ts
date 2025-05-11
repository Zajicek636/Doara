import {Component, OnInit} from '@angular/core';
import {BreadcrumbService, IBreadCrumb} from './breadcrumb.service';
import {Router, RouterLink} from '@angular/router';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss'],
  standalone: true,
  imports: [
    NgIf,
    NgForOf
  ]
})
export class BreadcrumbComponent implements OnInit {
  breadcrumbs: IBreadCrumb[] = [];
  constructor(private bcService: BreadcrumbService, private router: Router) {}
  ngOnInit(): void {
    this.bcService.breadcrumbs$.subscribe(b => this.breadcrumbs = b);
  }

  navigateTo(breadcrumb: IBreadCrumb, event: MouseEvent): void {
    event.preventDefault();

    const index = this.breadcrumbs.findIndex(b => b.url === breadcrumb.url);
    if (index === -1) {
      this.router.navigateByUrl(breadcrumb.url);
      return;
    }

    const preserved = this.breadcrumbs.slice(0, index);
    this.router.navigateByUrl(breadcrumb.url, {
      state: { previousBreadcrumbs: preserved }
    });
  }
}
