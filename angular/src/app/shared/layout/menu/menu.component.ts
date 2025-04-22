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
    { name: 'Sklady', link: 'sklady', icon: 'dataset', items: [{ name: 'Test', link: 'polozky', icon: 'data_thresholding' },{ name: 'Test2', link: 'editace-skladu', icon: 'data_thresholding'}]},
    { name: 'Účetnictví', link: 'ucetnictvi', icon: 'data_thresholding', items: [{ name: 'Nová fakura', link: 'nova-faktura', icon: 'plus' },{ name: 'Faktury', link: 'seznam-faktur', icon: 'data_thresholding' }]},
    { name: 'Settings', link: 'nastaveni', icon: 'settings' },
  ]) ;

  constructor(private router: Router) {}

  ngOnInit(): void
  {
  }
}
