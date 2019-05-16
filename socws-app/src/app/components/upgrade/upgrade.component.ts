import { Component, OnInit } from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {User} from "../../models/user";

@Component({
  selector: 'app-upgrade',
  templateUrl: './upgrade.component.html',
  styleUrls: ['./upgrade.component.css']
})
export class UpgradeComponent implements OnInit {

  user: User;

  constructor(private userService: UserService, private router: Router) { }

  async ngOnInit() {
    this.user = await this.userService.getLoggedUser();
  }

  upgrade(){
    this.userService.updateRole(this.user, "admin");
    this.router.navigate(['/']);
  }

  downgrade(){
    this.userService.updateRole(this.user, "user");
    this.router.navigate(['/']);
  }

  return(){
    this.router.navigate(['/']);
  }

}
