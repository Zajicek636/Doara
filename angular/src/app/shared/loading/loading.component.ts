import { Component } from '@angular/core';
import {LoadingService} from './loading.service';
import {Observable} from 'rxjs';
@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  standalone: false,
  styleUrl: './loading.component.scss'
})
export class LoadingComponent {
  loading$!: Observable<boolean>;
  constructor(private loadingService: LoadingService) {
    this.loading$ = this.loadingService.loading$
  }
}
