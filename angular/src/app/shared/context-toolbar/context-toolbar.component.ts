import {AfterViewInit, Component, ElementRef, HostListener, Input, OnInit} from '@angular/core';
import {ToolbarButton} from './context-toolbar.interfaces';

@Component({
  selector: 'app-context-toolbar',
  standalone: false,
  templateUrl: './context-toolbar.component.html',
  styleUrl: './context-toolbar.component.scss'
})
export class ContextToolbarComponent<T>  implements AfterViewInit, OnInit {
  @Input() buttons: ToolbarButton[] = [];
  @Input() contextItem?: T;

  showText = true;

  @HostListener('window:resize')
  onWindowResize() {
    this.checkSize();
  }

  ngOnInit() {
    this.checkSize();
  }

  constructor(private elRef: ElementRef) {}

  ngAfterViewInit(): void {
    this.checkSize();
  }

  private checkSize(): void {
    const element = this.elRef.nativeElement.querySelector('mat-toolbar') as HTMLElement;
    if (!element) return;

    const scrollWidth = element.scrollWidth;
    const clientWidth = element.clientWidth;
    this.showText = scrollWidth <= clientWidth;
  }

}
