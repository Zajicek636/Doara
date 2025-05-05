import {HttpClient, HttpHeaders} from '@angular/common/http';
import {lastValueFrom, Observable, tap} from 'rxjs';
import {Injectable} from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly apiUrl = 'https://localhost:44346';
  private readonly tokenKey = 'token';

  private readonly autoLoginCredentials = {
    username: 'admin',
    password: '1q2w3E*'
  };

  constructor(private http: HttpClient) {}

  async autoLogin(): Promise<void> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'accept': 'text/plain',
      'RequestVerificationToken': this.getRequestVerificationToken() ?? '',
      'X-Requested-With': 'XMLHttpRequest'
    });

    const body = {
      userNameOrEmailAddress: this.autoLoginCredentials.username,
      password: this.autoLoginCredentials.password,
      rememberMe: true
    };

    try {
      const response = await lastValueFrom(
        this.http.post<any>(`${this.apiUrl}/api/account/login`, body, {
          headers,
          withCredentials: true
        })
      );

      console.log('[AUTH] Přihlášení OK', response);
    } catch (err) {
      console.error('[AUTH] Chyba při přihlášení:', err);
    }
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  private getRequestVerificationToken(): string | null {
    const match = document.cookie.match(new RegExp('(^| )RequestVerificationToken=([^;]+)'));
    return match ? match[2] : null;
  }
}
