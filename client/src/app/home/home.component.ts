import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  posts: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('http://localhost:5135/api/posts').subscribe(
      response => {this.posts = response; },
      error => {console.log(error)}
    );
  }

}