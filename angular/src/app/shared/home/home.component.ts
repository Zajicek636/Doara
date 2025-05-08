import { Component } from '@angular/core';
import { WidgetComponent } from './widget/widget.component';
import { Widget } from './home.interfaces';
import {NgForOf, NgStyle} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';

export interface WidgetPolozky {
  id: number;
  pocetPolozek: number;
  nejPolozky: string;
}

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  constructor(private router:Router,) {
  }
  public navigate(route: string) {
    this.router.navigate([route]);
  }
}
