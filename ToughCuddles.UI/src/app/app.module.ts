import * as $ from 'jquery';
import { ROUTES } from './app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { AppComponent } from './app.component';
import { AppService } from './app.service';
import { MaterialModule, MdTabsModule } from '@angular/material';
import { Md2Module } from 'md2/module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RouterModule, NoPreloading } from '@angular/router';

import { ChartModule } from './shared/chart/chart.module';

import { Client, API_BASE_URL } from './shared/api/client.service';
import { BaseConfiguration } from './shared/api/base-configuration.service';
import { ViewContestantsComponent } from './pages/contestants/view-contestants/view-contestants.component';
import { ContestantsService } from './pages/contestants/contestants.service';
import { EditContestantComponent } from './pages/contestants/edit-contestant/edit-contestant.component';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { AdminDashboardService } from './pages/admin-dashboard/admin-dashboard.service';
import { ViewMatchesComponent } from './pages/matches/view-matches/view-matches.component';
import { MatchDetailsComponent } from './pages/matches/match-details/match-details.component';
import { MatchesService } from './pages/matches/matches.service';



@NgModule({
  declarations: [
    // Page
    AppComponent,
    ViewContestantsComponent,
    EditContestantComponent,
    AdminDashboardComponent,
    ViewMatchesComponent,
    MatchDetailsComponent
  ],
  imports: [
    // Angular Imports
    BrowserModule,
    BrowserAnimationsModule,
    MdTabsModule,
    FormsModule,
    HttpModule,
    ChartModule,

    RouterModule.forRoot(ROUTES, { useHash: true, preloadingStrategy: NoPreloading }),
    MaterialModule,
    FlexLayoutModule,
    Md2Module
  ],
  providers: [
    // Global service (Global state)
    AppService, ContestantsService, AdminDashboardService, MatchesService,
    Client, BaseConfiguration, { provide: API_BASE_URL, useValue: 'http://localhost:54903' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
