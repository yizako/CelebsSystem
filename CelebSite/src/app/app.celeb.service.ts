import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';


@Injectable()
export class CelebService {

  constructor(private http: HttpClient) { }
  public results;

  // search function
  getResults(): Promise<any> {
    let url = 'http://localhost:80/CelebsApi/api/Celebs';
    return this.http.get(url).toPromise();
  }

  resetAll(): Promise<any> {
    let url = 'http://localhost:80/CelebsApi/api/celebs/reset';
    return this.http.get(url).toPromise();
  }

  remove(index: number): Promise<any> {
    let url = 'http://localhost:80/CelebsApi/api/celeb/remove/' + index.toString();
    return this.http.get(url).toPromise();
  }


}
