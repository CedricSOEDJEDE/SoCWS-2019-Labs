import {Component} from '@angular/core';
import {User} from './models/user';
import {UserService} from "./services/user.service";
import {VeloService} from "./services/velo.service";

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  connectedUser: User;

  currentRoute: string;
  isLogged = false;

  constructor(private veloService: VeloService, private userService: UserService) {
    this.currentRoute = '';
  }

  async ngOnInit() {
    this.connectedUser = this.userService.getLoggedUser();
    console.log(this.connectedUser);
    this.isLogged = (this.connectedUser !== null && this.connectedUser !== undefined);
  }

  disconnect() {
    // this.userService.disconnectUser();
  }
}
