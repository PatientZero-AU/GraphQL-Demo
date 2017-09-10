import { Injectable } from '@angular/core';
import { Client, GraphQlQueryDto } from '../../shared/api/client.service';
import StatsQueryNode from 'raw-loader!./queries/avg-wins-tickets.gql';

@Injectable()
export class AdminDashboardService {

  constructor(private _apiClient: Client) { }

  public async getAverageWinsVsTicketSaleStats(): Promise<any>{
    const dto = new GraphQlQueryDto();
    dto.init({
      query: StatsQueryNode
    });
    return this._apiClient.apiGraphQlPost(dto).first().toPromise();
  }

}
