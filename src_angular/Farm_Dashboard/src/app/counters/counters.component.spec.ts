/* tslint:disable:no-unused-variable */

import { By }           from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import {
  beforeEach, beforeEachProviders,
  describe, xdescribe,
  expect, it, xit,
  async, inject
} from '@angular/core/testing';

import { CountersComponent } from './counters.component';

describe('Component: Counters', () => {
  it('should create an instance', () => {
    let component = new CountersComponent();
    expect(component).toBeTruthy();
  });
});
