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
          { name: 'Sklad', link: 'polozky', icon: BaseMaterialIcons.CATEGORY},
        ]
    },
    { name: 'Účetnictví', link: 'ucetnictvi', icon: BaseMaterialIcons.PAYMENTS, items:
        [
          { name: 'Doklad', link: 'doklad', icon: BaseMaterialIcons.NEW_QUOTE },
          { name: 'Seznam dokladů', link: 'seznam-dokladu', icon: BaseMaterialIcons.LIST_ICON},
          { name: 'Subjekty', link: 'subjekty', icon: BaseMaterialIcons.GROUPS_PEOPLE},
        ]
    },
  ]) ;

  constructor(private router: Router) {}

  ngOnInit(): void
  {
  }
}
