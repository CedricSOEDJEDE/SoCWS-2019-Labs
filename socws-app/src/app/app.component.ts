import {Component} from '@angular/core';
import {User} from './models/user';
import {UserService} from "./services/user.service";
import {VeloService} from "./services/velo.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  connectedUser: User;

  currentRoute: string;
  isLogged = false;

  constructor(private veloService: VeloService, private userService: UserService, private router: Router) {
    this.currentRoute = '';
  }

  async ngOnInit() {
    this.isLogged = (this.connectedUser !== null && this.connectedUser !== undefined);

    this.connectedUser = await this.userService.getLoggedUser();
    this.isLogged = (this.connectedUser !== null && this.connectedUser !== undefined);
    (await this.userService.streamLoggedUser()).subscribe(user => {
      this.connectedUser = user;
      this.isLogged = (this.connectedUser !== null && this.connectedUser !== undefined);
      if (user === null && this.connectedUser !== null) {
        // Disconnect from user
        this.router.navigate(['/']);
      }
    });
  }

  disconnect() {
    this.userService.disconnectUser();
  }
}
