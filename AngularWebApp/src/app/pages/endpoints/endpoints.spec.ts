import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Endpoints } from './endpoints';

describe('Endpoints', () => {
  let component: Endpoints;
  let fixture: ComponentFixture<Endpoints>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Endpoints],
    }).compileComponents();

    fixture = TestBed.createComponent(Endpoints);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
