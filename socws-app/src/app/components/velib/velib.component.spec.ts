import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VelibComponent } from './velib.component';

describe('VelibComponent', () => {
  let component: VelibComponent;
  let fixture: ComponentFixture<VelibComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VelibComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VelibComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
