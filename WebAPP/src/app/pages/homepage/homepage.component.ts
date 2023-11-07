import { Component, Inject, NgZone, OnInit, PLATFORM_ID, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HomePageService } from './homepage.service';
import { DoashBoardTotalDto } from 'src/app/models/DoashBoardTotalDto';
import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart,
  ChartComponent
} from "ng-apexcharts";


export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: string[];
};

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css'],
})
export class HomepageComponent implements OnInit {

  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;

  label = [];
  series = [];
  danhSach = new DoashBoardTotalDto;
  constructor(private jwtHelper: JwtHelperService, private router: Router,
    private service: HomePageService,) { }


  ngOnInit(): void {
    this.service.getDashBoard().subscribe(val => {
      this.danhSach = val;
    })
    this.service.getDashBoardChart().subscribe(data => {
      const seriesArr: number[] = [];
      const labelArr: string[] = [];
      data.forEach(item => {
        seriesArr.push(item.value);
        labelArr.push(item.category);
      });
      this.chartOptions = {
        series: seriesArr,
        chart: {
          width: 380,
          type: "pie"
        },
        labels: labelArr,
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200
              },
              legend: {
                position: "center"
              }
            }
          }
        ]
      };
    })
  }

  isUserAuthenticated() {
    const token = localStorage.getItem('jwt');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    } else {
      return false;
    }
  }

  public logOut = () => {
    localStorage.removeItem('jwt');
  };
}
