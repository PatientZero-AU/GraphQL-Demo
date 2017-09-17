import { Injectable } from '@angular/core';
import { Client, GraphQlQueryDto } from '../../shared/api/client.service';
import AllTeamsQueryNode from 'raw-loader!./queries/all-teams.gql';
import ContestantQueryNode from 'raw-loader!./queries/contestant.gql';

@Injectable()
export class ContestantsService {

  constructor(private _apiClient: Client) { }

  public async getContestants(): Promise<any> {

    const dto = new GraphQlQueryDto();
    dto.init({
      query: AllTeamsQueryNode // 3) Raw Query
    });

    // 4) Send Request to Server
    return this._apiClient.apiGraphQlPost(dto).first().toPromise();
  }

  public async getContestant(id: string): Promise<any> {
    const dto = new GraphQlQueryDto();
    dto.init({
      query: ContestantQueryNode,
      variables: {
        contestantId: id
      }
    });
    return this._apiClient.apiGraphQlPost(dto).first().toPromise();
  }
}
