import {Component, Input} from '@angular/core';
import {ToolbarButton} from './context-toolbar.interfaces';

@Component({
  selector: 'app-context-toolbar',
  standalone: false,
  templateUrl: './context-toolbar.component.html',
  styleUrl: './context-toolbar.component.scss'
})
export class ContextToolbarComponent<T> {
  /** Pole tlačítek, definovaných v rodiči */
  @Input() buttons: ToolbarButton<T>[] = [];
  /** Volitelný kontext (např. vybraný řádek) */
  @Input() contextItem?: T;
}
