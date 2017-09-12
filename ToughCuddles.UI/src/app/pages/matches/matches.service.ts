import { Injectable } from '@angular/core';
import { Client, GraphQlQueryDto } from '../../shared/api/client.service';
import AllMatchesQueryNode from 'raw-loader!./queries/all-matches.gql';
import MatchQueryNode from 'raw-loader!./queries/match.gql';

@Injectable()
export class MatchesService {

  constructor(private _apiClient: Client) { }
  public async getMatches(): Promise<any> {

    const dto = new GraphQlQueryDto();
    dto.init({
      query: AllMatchesQueryNode
    });
    return this._apiClient.apiGraphQlPost(dto).first().toPromise();
  }

  public async getMatch(id: string): Promise<any> {
    const dto = new GraphQlQueryDto();
    dto.init({
      query: MatchQueryNode,
      variables: {
        matchId: id
      }
    });
    return this._apiClient.apiGraphQlPost(dto).first().toPromise();
  }
}
