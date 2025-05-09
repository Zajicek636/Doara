import {Component, Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CacheService {
  private cache = new Map<string, any>();

  /**
   * Uloží data pod klíč.
   */
  set<T>(key: string, data: T): void {
    this.cache.set(key, data);
  }

  /**
   * Vrátí data z cache, pokud existují.
   */
  get<T>(key: string): T | null {
    return this.cache.has(key) ? this.cache.get(key) : null;
  }

  /**
   * Zjistí, zda data pod klíčem existují.
   */
  has(key: string): boolean {
    return this.cache.has(key);
  }

  /**
   * Odstraní data z cache.
   */
  delete(key: string): void {
    this.cache.delete(key);
  }

  /**
   * Vymaže celou cache.
   */
  clear(): void {
    this.cache.clear();
  }

  /**
   * Pokud data nejsou v cache, zavolá fetcherFn a uloží je do cache.
   * Jinak vrátí data z cache.
   */
  async getOrFetch<T>(key: string, fetcherFn: () => Promise<T>): Promise<T> {
    if (this.has(key)) {
      return this.get<T>(key)!;
    }
    const data = await fetcherFn();
    this.set<T>(key, data);
    return data;
  }
}
