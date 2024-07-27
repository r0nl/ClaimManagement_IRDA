import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  today: string = new Date().toLocaleDateString('en-US', {
    year:'numeric',
    month: 'long',
    day: 'numeric'
  });
}
