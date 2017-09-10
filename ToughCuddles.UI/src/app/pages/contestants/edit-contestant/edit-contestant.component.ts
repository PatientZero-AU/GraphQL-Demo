import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { ContestantsService } from '../contestants.service';
import { ContestantQuery } from '../../../shared/api/graphql/schema';

@Component({
  selector: 'app-edit-contestant',
  templateUrl: './edit-contestant.component.html',
  styleUrls: ['./edit-contestant.component.css']
})
export class EditContestantComponent implements OnInit {

  data: ContestantQuery;
  constructor(
    private _service: ContestantsService,
    private router: Router,
    private route: ActivatedRoute) { }

  async ngOnInit() {
    const result = await this.route.paramMap
      .switchMap((params: ParamMap) => {
        const id: string = params.get('id');
        return this._service.getContestant(id);
      }).first().toPromise();
    this.data = result.data;
  }

  goBack() {
    this.router.navigate(['view-contestants']);
  }
}
