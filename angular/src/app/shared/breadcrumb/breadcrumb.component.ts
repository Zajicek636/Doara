import {Component, OnInit} from '@angular/core';
import {BreadcrumbService, IBreadCrumb} from './breadcrumb.service';
import {RouterLink} from '@angular/router';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss'],
  standalone: true,
  imports: [
    RouterLink,
    NgIf,
    NgForOf
  ]
})
export class BreadcrumbComponent implements OnInit {
  breadcrumbs: IBreadCrumb[] = [];

  constructor(private bcService: BreadcrumbService) {}

  ngOnInit(): void {
    this.bcService.breadcrumbs$.subscribe(b => this.breadcrumbs = b);
  }
}
