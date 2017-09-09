import { Injectable } from '@angular/core';
import { Client, GraphQlQueryDto } from '../../shared/api/client.service';

@Injectable()
export class MatchesService {

  constructor(private _apiClient: Client) { }

}
