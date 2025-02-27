import { Component, OnInit } from '@angular/core';
import { SkladyService } from '../services/sklady.service';

@Component({
  selector: 'lib-sklady',
  template: ` <p>sklady works!</p> `,
  styles: [],
})
export class SkladyComponent implements OnInit {
  constructor(private service: SkladyService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
