import { Component, OnInit } from '@angular/core';
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: User;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.user = new User('','','','','', '');
  }

  register() {
    this.user.mail = this.user.mail.trim();
    this.userService.registerUser(this.user).then(() => {
      this.router.navigate(['/']);
    }).catch(error => {console.log(error);});
  }

}
