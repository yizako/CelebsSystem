import { Component, OnInit } from '@angular/core';
import { Celeb } from './app.celeb';
import { CelebService } from './app.celeb.service';
import { DomSanitizer } from '@angular/platform-browser';
import { transformAll } from '@angular/compiler/src/render3/r3_ast';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CelebSite';
  celebs: Celeb[] = [];
  wait: boolean = false;

  constructor(private celebService: CelebService, public sanitizer: DomSanitizer) {
  }

  ngOnInit() {
    this.getCelebs();
  }

  getCelebs() {
    this.wait = true;
    this.celebService.getResults().then((data => { // call search service
      this.celebs = JSON.parse(data);
      this.wait = false;
    }));
  }

  resetAll() {
    this.wait = true;
    this.celebService.resetAll().then((data => { // call search service
      this.celebs = JSON.parse(data);
      this.wait = false;
    }));
  }

  remove(index: number) {
    this.celebService.remove(index).then((data => { // call search service
      this.celebs = JSON.parse(data);
    }));
  }
}
