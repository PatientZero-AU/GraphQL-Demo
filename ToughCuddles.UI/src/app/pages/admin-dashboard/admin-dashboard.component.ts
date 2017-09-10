import { Component, OnInit } from '@angular/core';

import { AdminDashboardService } from './admin-dashboard.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  mock: any = ChartMock;

  constructor(private _service: AdminDashboardService) { }

  async ngOnInit() {
  }
}

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
