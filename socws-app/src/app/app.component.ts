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

  constructor(private veloService: VeloService) {
    this.currentRoute = '';
  }

  async ngOnInit() {
    // this.isLogged = (this.connectedUser !== null && this.connectedUser !== undefined);

    /*this.router.events.subscribe(event => {
      if (event instanceof RoutesRecognized) {
        this.currentRoute = event.url;
      }
    });*/

    /*this.connectedUser = await this.userService.getLoggedUser();
    this.isLogged = (this.connectedUser !== null && this.connectedUser !== undefined);
    (await this.userService.streamLoggedUser()).subscribe(user => {
      this.connectedUser = user;
      this.isLogged = (this.connectedUser !== null && this.connectedUser !== undefined);
      if (user === null && this.connectedUser !== null) {
        // Disconnect from user
        this.router.navigate(['/']);
      }
    });

    //  if(this.isLogged) {
    // this.notificationService.getPermission(this.connectedUser);
    // }

    // Open popup to inform of cookie using
    if (localStorage.getItem('knowCookies') !== 'true') {
      setTimeout(() => {
        // Must open only if the user never said ok
        this.popupService.openCookiePopup();
      }, 1500);
    }*/
  }

  test() {
    this.veloService.getContracts();
  }

  disconnect() {
    // this.userService.disconnectUser();
  }
}
