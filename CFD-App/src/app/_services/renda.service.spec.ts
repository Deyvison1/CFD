import { TestBed } from '@angular/core/testing';

import { RendaService } from './renda.service';

describe('RendaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RendaService = TestBed.get(RendaService);
    expect(service).toBeTruthy();
  });
});
