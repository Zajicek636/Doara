import {Injectable} from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class AuthInitializerService {
  constructor(private authService: AuthService) {}

  async init(): Promise<void> {
    await this.authService.auth();
  }
}
