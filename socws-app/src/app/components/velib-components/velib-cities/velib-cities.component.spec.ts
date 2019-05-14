import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VelibCitiesComponent } from './velib-cities.component';

describe('VelibCitiesComponent', () => {
  let component: VelibCitiesComponent;
  let fixture: ComponentFixture<VelibCitiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VelibCitiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VelibCitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
