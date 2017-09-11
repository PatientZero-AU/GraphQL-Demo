import { Injectable } from '@angular/core';
import { Client, GraphQlQueryDto } from '../../shared/api/client.service';
import StatsQueryNode from 'raw-loader!./queries/avg-wins-tickets.gql';
import VenueQueryNode from 'raw-loader!./queries/venue-ticket-sales.gql';

@Injectable()
export class AdminDashboardService {

  constructor(private _apiClient: Client) { }

  public async getAverageWinsVsTicketSaleStats(): Promise<any> {
    const dto = new GraphQlQueryDto();
    dto.init({
      query: StatsQueryNode
    });
    return this._apiClient.apiGraphQlPost(dto).first().toPromise();
  }

  public async getVenueTicketSales(): Promise<any> {
    const dto = new GraphQlQueryDto();
    dto.init({
      query: VenueQueryNode
    });
    return this._apiClient.apiGraphQlPost(dto).first().toPromise();
  }
}
