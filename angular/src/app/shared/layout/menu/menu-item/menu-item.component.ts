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
//TODO PRIDAT NESTED MENU REKURZIVNE
//TODO PRIDAT NESTED MENU REKURZIVNE
//TODO PRIDAT NESTED MENU REKURZIVNE
//TODO PRIDAT NESTED MENU REKURZIVNE
//TODO PRIDAT NESTED MENU REKURZIVNE
//TODO PRIDAT NESTED MENU REKURZIVNE
//TODO PRIDAT NESTED MENU REKURZIVNE
  constructor(private router: Router) {}

  // Toggle pro otevření/uzavření pod-položek
 /* toggle(): void {
    if (this.item.items?.length) {
      this.opened = !this.opened;
    } else {
      this.router.navigate([this.item.link]);  // Přechod na stránku, pokud není pod-položka
    }
  }*/

  activate(item: any) {
    if (item.link !== null) {
      if(item.items) {
        this.nestedItemsOpen = signal(true);
      }
      this.router.navigate([item.link]);
    }
  }

  // Zjištění, zda je položka vybraná
  isSelected(): boolean {
    return this.router.url.includes(`/${this.item.link}`);
  }
}
