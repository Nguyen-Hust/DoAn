import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HomePageService } from './homepage.service';
import { DoashBoardTotalDto } from 'src/app/models/DoashBoardTotalDto';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css'],
})
export class HomepageComponent implements OnInit {

  danhSach = new DoashBoardTotalDto;
  constructor(private jwtHelper: JwtHelperService, private router: Router,
    private service: HomePageService) {}
  
  
  ngOnInit(): void {
    this.service.getDashBoard().subscribe(val => {
      this.danhSach = val;
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
