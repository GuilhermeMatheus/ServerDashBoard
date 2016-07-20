import { provideRouter, RouterConfig }  from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';

const routes: RouterConfig = [
  {
    path: '',
    redirectTo: '/dashboards'
    //pathMatch: ['full']
  },
  {
    path: 'dashboards',
    component: DashboardComponent
  },
  {
    path: 'servers',
    component: DashboardComponent
  },
  {
    path: 'widgets',
    component: DashboardComponent
  },
  {
    path: 'utilities',
    component: DashboardComponent
  }
];

export const appRouterProviders = [
  provideRouter(routes)
];


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/
