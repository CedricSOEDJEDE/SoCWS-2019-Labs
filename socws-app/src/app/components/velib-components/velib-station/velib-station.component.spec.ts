import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VelibStationComponent } from './velib-station.component';

describe('VelibStationComponent', () => {
  let component: VelibStationComponent;
  let fixture: ComponentFixture<VelibStationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VelibStationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VelibStationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
