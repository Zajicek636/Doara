import {Component, Input} from '@angular/core';
import {ToolbarButton} from './context-toolbar.interfaces';

@Component({
  selector: 'app-context-toolbar',
  standalone: false,
  templateUrl: './context-toolbar.component.html',
  styleUrl: './context-toolbar.component.scss'
})
export class ContextToolbarComponent<T> {
  @Input() buttons: ToolbarButton[] = [];
  @Input() contextItem?: T;
}
