import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatchesService } from '../matches.service';
import * as moment from 'moment';
import { AllMatchesQuery } from '../../../shared/api/graphql/schema';

@Component({
  selector: 'app-view-matches',
  templateUrl: './view-matches.component.html',
  styleUrls: ['./view-matches.component.css']
})
export class ViewMatchesComponent implements OnInit {

  data: AllMatchesQuery;
  constructor(
    private _service: MatchesService,
    private router: Router) { }

  async ngOnInit() {
    const result = await this._service.getMatches();
    this.data = result.data;
  }

  toDateString(date: string): string {
    return moment(new Date(date)).format('DD/MM/YYYY');
  }

  matchDetail(id: string) {
    this.router.navigate(['match-details', id]);
  }
}
