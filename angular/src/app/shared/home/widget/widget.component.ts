import { Component, Input } from '@angular/core';
import {NgStyle} from '@angular/common';

export interface Widget<T> {
  id: number;
  label: string;
  height: number;
  width: number;
  content: T;
}

@Component({
  selector: 'app-widget',
  templateUrl: './widget.component.html',
  imports: [
    NgStyle
  ],
  styleUrls: ['./widget.component.scss']
})
export class WidgetComponent {
  @Input() data: any | undefined;

  onDragStart(event: DragEvent): void {
    console.log('Dragging started for item:');
    //event.dataTransfer?.setData('text/plain', JSON.stringify(item));
  }
}
