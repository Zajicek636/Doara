import { TestBed } from '@angular/core/testing';
import { ActivatedRoute, ActivatedRouteSnapshot, NavigationEnd, Router } from '@angular/router';
import {BehaviorSubject, take} from 'rxjs';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';


describe('BreadcrumbService', () => {
  let service: BreadcrumbService;
  let events$: BehaviorSubject<any>;
  let mockRouter: Partial<Router>;
  let mockActivatedRoute: ActivatedRoute;

  const createActivatedRouteMock = (data: any, path: string, children: ActivatedRoute[] = []): ActivatedRoute => {
    const snapshot = {
      url: [{ path }],
      data
    } as Partial<ActivatedRouteSnapshot>;

    return {
      snapshot: snapshot as ActivatedRouteSnapshot,
      children,
      outlet: 'primary'
    } as ActivatedRoute;
  };

  beforeEach(() => {
    events$ = new BehaviorSubject<any>(new NavigationEnd(1, '/test', '/test'));
    mockRouter = {
      events: events$.asObservable()
    };

    Object.defineProperty(window, 'history', {
      value: {
        ...window.history,
        state: {
          previousBreadcrumbs: [{ label: 'Previous', url: '/previous' }]
        }
      },
      writable: true
    });

    const detailRoute = createActivatedRouteMock({ breadcrumb: 'Detail' }, 'detail');
    const sectionRoute = createActivatedRouteMock({ breadcrumb: 'Section' }, 'section', [detailRoute]);
    const rootRoute = createActivatedRouteMock({}, '', [sectionRoute]);

    mockActivatedRoute = {
      root: rootRoute
    } as ActivatedRoute;

    TestBed.configureTestingModule({
      providers: [
        { provide: Router, useValue: mockRouter },
        { provide: ActivatedRoute, useValue: mockActivatedRoute }
      ]
    });

    service = TestBed.inject(BreadcrumbService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should build and emit breadcrumbs from route and history', (done) => {
    service.breadcrumbs$.pipe(take(1)).subscribe((breadcrumbs: any) => {
      expect(breadcrumbs.length).toBe(3);

      expect(breadcrumbs[0]).toEqual({ label: 'Previous', url: '/previous' });
      expect(breadcrumbs[1]).toEqual({ label: 'Section', url: '/section' });
      expect(breadcrumbs[2]).toEqual({ label: 'Detail', url: '/section/detail' });
      done();
    });

    events$.next(new NavigationEnd(2, '/section/detail', '/section/detail'));
  });
});
