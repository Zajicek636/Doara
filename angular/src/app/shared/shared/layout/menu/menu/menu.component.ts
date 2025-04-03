import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  standalone: false,
  styleUrl: './menu.component.scss'
})
export class MenuComponent implements OnInit {
  menuItems: {}[] = [
    { name: 'Domů', link: 'home', icon: 'cml-home' },
    { name: 'Účetní jednotky', link: 'ucetni-jednotky', icon: 'cml-cog' },
    { name: 'Uživatelé', link: 'uzivatele', icon: 'cml-user-replace' },
    { name: 'Role', link: 'role', icon: 'cml-legal' },
    { name: 'CRORegistration', link: 'cro-registrace', icon: 'cml-wrench' },
    { name: 'Přenos účetní jednotky', link: 'prenos-ucetni-jednotky', icon: 'cml-move-transfer-file' },
    { name: 'Členění pro výkazy', link: 'cleneni-pro-vykazy', icon: 'cml-sitemap' },
    { name: 'Kontace', link: 'kontace', icon: 'cml-books' },
  ];

  constructor(private router: Router) { }

  ngOnInit(): void
  {

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
