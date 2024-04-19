import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { error } from 'console';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit {
  title = 'BlogBox';
  //weatherForecasts: any;
  posts: any;

  constructor() 
  {

  }

  ngOnInit(): void
  {
  }
}