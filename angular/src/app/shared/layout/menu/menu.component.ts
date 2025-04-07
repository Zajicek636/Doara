import {Component, Input, OnInit, signal} from '@angular/core';
import {Router} from '@angular/router';

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
    { name: 'Sklady', link: 'sklady', icon: 'dataset' },
    { name: 'Účetnictví', link: 'ucetnictvi', icon: 'data_thresholding' },
    { name: 'Settings', link: 'nastaveni', icon: 'settings' },
  ]) ;

  constructor(private router: Router) { }

  ngOnInit(): void
  {

  }

  openedItem: MenuItem | null = null;

  toggleSubmenu(item: MenuItem): void {
    if (this.openedItem === item) {
      this.openedItem = null;
    } else {
      this.openedItem = item;
    }
  }

  activate(item: any) {
    if (item.link !== null) {
      this.router.navigate([item.link]);
    }
  }

  public isSelected(item: any)
  {
    return this.router.url.includes(`/${item.link}`)
  }
}
