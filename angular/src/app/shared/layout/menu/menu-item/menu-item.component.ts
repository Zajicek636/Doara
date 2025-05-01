import {Component, EventEmitter, Input, Output, signal, Signal} from '@angular/core';
import {MenuItem} from '../menu.component';
import {Router} from '@angular/router';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  standalone: false,
  styleUrl: './menu-item.component.scss'
})
export class MenuItemComponent {
  @Input() item!: MenuItem;
  @Input() collapsed = false;

  nestedItemsOpen = signal(false);
  selectedItem: MenuItem | undefined ;

  constructor(private router: Router) {}


  activate(item: MenuItem) {
    this.selectedItem = item
    if(item.items) {
      this.nestedItemsOpen.update(prev => !prev);
    } else if(!item.items) {
    }
  }


}
