import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AllTeamsQuery } from '../../../shared/api/graphql/schema';
import { ContestantsService } from '../contestants.service';

@Component({
  selector: 'app-view-contestants',
  templateUrl: './view-contestants.component.html',
  styleUrls: ['./view-contestants.component.css']
})
export class ViewContestantsComponent implements OnInit {

  data: AllTeamsQuery;
  constructor(
    private _service: ContestantsService,
    private router: Router) { }

  async ngOnInit() {
    const result = await this._service.getContestants();
    this.data = result.data;
  }

  editContestant(id: string) {
    this.router.navigate(['edit-contestant', id]);
  }
}
