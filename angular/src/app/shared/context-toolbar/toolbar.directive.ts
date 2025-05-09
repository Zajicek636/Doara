import {AfterViewInit, Directive, ElementRef, EventEmitter, Output} from '@angular/core';

@Directive({
  selector: '[toolbarButtonDirective]'
})
export class ToolbarButtonDirective implements AfterViewInit {
  @Output() renderedBtn: EventEmitter<ElementRef> = new EventEmitter<ElementRef>();

  constructor(private el: ElementRef) {}

  ngAfterViewInit() {
    this.renderedBtn.emit(this.el);
  }
}
