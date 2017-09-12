import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { MatchesService } from '../matches.service';
import { MatchQuery } from '../../../shared/api/graphql/schema';

@Component({
  selector: 'app-match-details',
  templateUrl: './match-details.component.html',
  styleUrls: ['./match-details.component.css']
})
export class MatchDetailsComponent implements OnInit {

  data: MatchQuery;
  constructor(
    private _service: MatchesService,
    private router: Router,
    private route: ActivatedRoute) { }

  async ngOnInit() {
    const result = await this.route.paramMap
      .switchMap((params: ParamMap) => {
        const id: string = params.get('id');
        return this._service.getMatch(id);
      }).first().toPromise();
    this.data = result.data;
  }
}
