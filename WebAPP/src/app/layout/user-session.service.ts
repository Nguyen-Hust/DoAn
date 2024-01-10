import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";
import configurl from "../../assets/config/config.json";

@Injectable({
  providedIn: "root",
})
export class UserSessionService {
  url = configurl.apiServer.url + "/api/authentication/";
  private _user: any = null;
  private _user$ = new BehaviorSubject<any>(null);

  constructor(private http: HttpClient) {}

  get user(): any {
    return this._user;
  }

  get user$() {
    return this._user$;
  }

  getAccountBootstrap(): Observable<any> {
    return this.http
      .get(this.url + "get-user-info", {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
      })
      .pipe(
        tap((response) => {
          this.setUser(response);
        })
      );
  }

  private setUser(user) {
    this._user = user;
    this._user$.next(user);
  }
}
