import { Routes } from '@angular/router';

import { ViewMatchesComponent } from './pages/matches/view-matches/view-matches.component';
import { MatchDetailsComponent } from './pages/matches/match-details/match-details.component';
import { ViewContestantsComponent } from './pages/contestants/view-contestants/view-contestants.component';
import { EditContestantComponent } from './pages/contestants/edit-contestant/edit-contestant.component';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';

export const ROUTES: Routes = [
  {
    path: '',
    redirectTo: '/view-contestants',
    pathMatch: 'full'
  },
  {
    path: 'view-contestants',
    component: ViewContestantsComponent
  },
  {
    path: 'edit-contestant/:id',
    component: EditContestantComponent
  },
  {
    path: 'view-matches',
    component: ViewMatchesComponent
  },
  {
    path: 'match-details/:id',
    component: MatchDetailsComponent
  },
  {
    path: 'admin-dashboard',
    component: AdminDashboardComponent
  }
];
