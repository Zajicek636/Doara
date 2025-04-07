import {Component, Input} from '@angular/core';
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
  opened = false;

  constructor(private router: Router) {}

  // Toggle pro otevření/uzavření pod-položek
  toggle(): void {
    if (this.item.items?.length) {
      this.opened = !this.opened;
    } else {
      this.router.navigate([this.item.link]);  // Přechod na stránku, pokud není pod-položka
    }
  }

  // Zjištění, zda je položka vybraná
  isSelected(): boolean {
    return this.router.url.includes(`/${this.item.link}`);
  }
}
