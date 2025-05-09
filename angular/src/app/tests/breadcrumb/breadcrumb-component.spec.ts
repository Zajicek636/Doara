import { ComponentFixture, TestBed } from '@angular/core/testing';

import { of, BehaviorSubject } from 'rxjs';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import {BreadcrumbComponent} from '../../shared/breadcrumb/breadcrumb.component';
import {BreadcrumbService, IBreadCrumb} from '../../shared/breadcrumb/breadcrumb.service';


describe('BreadcrumbComponent', () => {
  let component: BreadcrumbComponent;
  let fixture: ComponentFixture<BreadcrumbComponent>;
  let breadcrumbService: jasmine.SpyObj<BreadcrumbService>;
  let breadcrumbs$: BehaviorSubject<IBreadCrumb[]>;

  const mockBreadcrumbs: IBreadCrumb[] = [
    { label: 'Home', url: '/home' },
    { label: 'Section', url: '/home/section' },
    { label: 'Detail', url: '/home/section/detail' }
  ];

  beforeEach(async () => {
    breadcrumbs$ = new BehaviorSubject<IBreadCrumb[]>([]);
    const spy = jasmine.createSpyObj<BreadcrumbService>('BreadcrumbService', [], {
      breadcrumbs$: breadcrumbs$.asObservable()
    });

    await TestBed.configureTestingModule({
      imports: [
        BreadcrumbComponent,
        RouterTestingModule
      ],
      providers: [
        { provide: BreadcrumbService, useValue: spy }
      ]
    }).compileComponents();

    breadcrumbService = TestBed.inject(BreadcrumbService) as jasmine.SpyObj<BreadcrumbService>;
    fixture = TestBed.createComponent(BreadcrumbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges(); // ngOnInit
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render breadcrumbs as links except the last one', () => {
    breadcrumbs$.next(mockBreadcrumbs);
    fixture.detectChanges();

    const breadcrumbItems = fixture.debugElement.queryAll(By.css('.breadcrumb-item'));
    expect(breadcrumbItems.length).toBe(3);

    //First two shall be a
    const linkElements = breadcrumbItems.slice(0, -1).map(item =>
      item.query(By.css('a'))
    );
    linkElements.forEach(link => expect(link).toBeTruthy());

    // Last should be <span>
    const lastItem = breadcrumbItems[2].query(By.css('span'));
    expect(lastItem.nativeElement.textContent.trim()).toBe('Detail');
  });

  it('should update breadcrumbs when service emits new values', () => {
    const updatedBreadcrumbs = [
      { label: 'New', url: '/new' }
    ];
    breadcrumbs$.next(updatedBreadcrumbs);
    fixture.detectChanges();

    const breadcrumbLabels = fixture.debugElement.queryAll(By.css('.breadcrumb-item'));
    expect(breadcrumbLabels.length).toBe(1);
    expect(breadcrumbLabels[0].nativeElement.textContent.trim()).toContain('New');
  });
});
