import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { AdminDashboardService } from './admin-dashboard.service';
import { AverageWinsTicketsQuery, VenueTicketSalesQuery } from '../../shared/api/graphql/schema';
import { ChartModel } from './chart.model';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  averageWins: AverageWinsTicketsQuery;
  ticketSales: VenueTicketSalesQuery;

  done = false;
  bar: any = {
    type: '',
    data: {}
  };

  line: any = {
    type: '',
    data: {}
  };

  options: any = {};

  constructor(private _service: AdminDashboardService) { }

  async ngOnInit() {
    const result: any = await this._service.getAverageWinsVsTicketSaleStats();
    this.averageWins = result.data;

    const result2: any = await this._service.getVenueTicketSales();
    this.ticketSales = result2.data;

    this.bar = {
      type: 'bar',
      data: {
        labels: this.averageWins.teams.map(t => t.name),
        datasets: [{
          label: '% Average Win Rate',
          borderColor: '#009688',
          backgroundColor: 'rgba(0, 150, 136, 0.5)',
          data: this.averageWins.teams.map(t => t.averageWinRate * 100)
        }, {
          label: 'Tickets Sold',
          borderColor: '#ffc107',
          backgroundColor: 'rgba(255, 193, 7, 0.5)',
          data: this.averageWins.teams.map(t => t.ticketsSoldCount)
        }]
      }
    };

    const dates = this.ticketSales
      .venues
      .map(v => v.ticketSales.map(t => {
        const date = new Date(t.item1);
        return moment(date).format('DD/MM/YYYY');
      }))
      .reduce((a, b) => a.concat(b));

    let arr = new Array<any>();
    for (let i = this.ticketSales.venues.length - 1; 0 <= i; --i) {
      const venue = this.ticketSales.venues[i];
      arr.push({
        label: venue.name,
        borderColor: '#009688',
        backgroundColor: 'rgba(0, 150, 136, 0.5)',
        data: venue.ticketSales.map(t => t.item2)
      });
    }

    console.log(arr);

    this.line = {
      type: 'line',
      data: {
        labels: Array.from(new Set(dates)),
        datasets: arr
      }
    };

    this.done = true;
  }
}

this.options = {
  scales: {
    yAxes: [{
      ticks: {
        beginAtZero: true
      }
    }]
  }
}
/*
class ChartMock {
  static line = {
    type: 'line',
    data: {
      labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
      datasets: [{
        label: 'My First dataset',
        borderColor: '#009688',
        backgroundColor: 'rgba(0, 150, 136, 0.5)',
        data: [ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor()]
      }, {
        label: 'My Second dataset',
        borderColor: '#ffc107',
        backgroundColor: 'rgba(255, 193, 7, 0.5)',
        data: [ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor()]
      }]
    }
  };

  static bar = {
    type: 'bar',
    data: {
      labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
      datasets: [{
        label: 'Credit',
        borderColor: '#009688',
        backgroundColor: 'rgba(0, 150, 136, 0.5)',
        data: [ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor()]
      }, {
        label: 'Debit',
        borderColor: '#ffc107',
        backgroundColor: 'rgba(255, 193, 7, 0.5)',
        data: [ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(), ChartMock.randomScalingFactor(),
        ChartMock.randomScalingFactor()]
      }]
    },
    options: {
      elements: {
        rectangle: {
          borderWidth: 2,
          borderColor: 'rgb(0, 255, 0)',
          borderSkipped: 'bottom'
        }
      },
    }
  };

  static randomScalingFactor() {
    return Math.round(Math.random() * 100);
  };
}
*/
