import { Component, OnInit } from '@angular/core';
import {VeloService} from "../../../services/velo.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-velib',
  templateUrl: './velib.component.html',
  styleUrls: ['./velib.component.css']
})
export class VelibComponent implements OnInit {

  citiesList: string[];

  constructor(private veloService: VeloService, private router: Router) {
    this.citiesList = ["Loading"];
  }

  ngOnInit() {
     this.veloService.getCities().then(res => this.citiesList = res);
  }

  goToCity(city: string){
    this.router.navigate(['/cities',city]);
  }

  reloadCities(){
    this.veloService.getCities().then(er => {console.log(er);
      this.citiesList = er;});
    console.log(this.citiesList);
  }


}
