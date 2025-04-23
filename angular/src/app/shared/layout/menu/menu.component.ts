import {Component, Input, OnInit, signal} from '@angular/core';
import {Router} from '@angular/router';
import {BaseMaterialIcons} from '../../../../styles/material.icons';

export interface  MenuItem {
  name: string;
  link: string;
  icon: string;
  items?: MenuItem[];
}
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  standalone: false,
  styleUrl: './menu.component.scss'
})
export class MenuComponent implements OnInit {
  @Input() collapsed = false;

  menuItems = signal<MenuItem[]>([
    { name: 'Sklady', link: 'sklady', icon: BaseMaterialIcons.CONTAINER, items:
        [
          { name: 'Test', link: 'polozky', icon: BaseMaterialIcons.GRAPH_INCREASE },
          { name: 'Test2', link: 'editace-skladu', icon: BaseMaterialIcons.GRAPH_INCREASE}
        ]
    },
    { name: 'Účetnictví', link: 'ucetnictvi', icon: BaseMaterialIcons.PAYMENTS, items:
        [
          { name: 'Nová fakura', link: 'nova-faktura', icon: BaseMaterialIcons.NEW_QUOTE },
          { name: 'Faktury', link: 'seznam-faktur', icon: BaseMaterialIcons.LIST_ICON},
          { name: 'Subjekty', link: 'subjekty', icon: BaseMaterialIcons.GROUPS_PEOPLE},
        ]
    },
    { name: 'Settings', link: 'nastaveni', icon: BaseMaterialIcons.COG_SETTINGS },
  ]) ;

  constructor(private router: Router) {}

  ngOnInit(): void
  {
  }
}
