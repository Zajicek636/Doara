import { Component } from '@angular/core';
import { WidgetComponent } from './widget/widget.component';
import { Widget } from './home.interfaces';
import {NgForOf, NgStyle} from '@angular/common';

export interface WidgetPolozky {
  id: number;
  pocetPolozek: number;
  nejPolozky: string;
}

@Component({
  selector: 'app-home',
  imports: [WidgetComponent, NgForOf, NgStyle],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  data: Widget<WidgetPolozky>[] = [
    {
      id: 1,
      label: "Test Widget 1",
      height: 220,
      width: 400,
      content: { id: 2122, pocetPolozek: 2, nejPolozky: "test" }
    },
    {
      id: 2,
      label: "Test Widget 2",
      height: 250,
      width: 500,
      content: { id: 2123, pocetPolozek: 5, nejPolozky: "another test" }
    },
    // Přidej další widgety podle potřeby
  ];
}
