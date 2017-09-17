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

  // 1) When Page loads
  async ngOnInit() {

    // 2) Request teams/contestants
    const result = await this._service.getContestants();

    // 5) Strongly typed result
    this.data = result.data;
  }

  editContestant(id: string) {
    this.router.navigate(['edit-contestant', id]);
  }
}
