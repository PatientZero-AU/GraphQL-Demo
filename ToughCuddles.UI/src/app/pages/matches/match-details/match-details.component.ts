import { Component, OnInit } from '@angular/core';
import { MatchesService } from '../matches.service';

@Component({
  selector: 'app-match-details',
  templateUrl: './match-details.component.html',
  styleUrls: ['./match-details.component.css']
})
export class MatchDetailsComponent implements OnInit {

  constructor(private _service: MatchesService) { }

  async ngOnInit() {
  }

}
