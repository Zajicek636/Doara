import {Component, Input} from '@angular/core';
import {ToolbarButton} from '../context-toolbar.interfaces';

@Component({
  selector: 'app-toolbar-button',
  standalone: false,
  templateUrl: './toolbar-button.component.html',
  styleUrls: ['./toolbar-button.component.scss']
})
export class ToolbarButtonComponent<T = any> {
  @Input() btn!: ToolbarButton<T>;
  @Input() contextItem?: T;
  @Input() showText = true;
}
