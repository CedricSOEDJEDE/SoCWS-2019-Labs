import { Component, OnInit } from '@angular/core';
import {VeloService} from "../../services/velo.service";

@Component({
  selector: 'app-velib',
  templateUrl: './velib.component.html',
  styleUrls: ['./velib.component.css']
})
export class VelibComponent implements OnInit {


  citiesList: string[];

  constructor(private veloService: VeloService) {
    this.citiesList = ["Loading"];
  }

  ngOnInit() {
     this.veloService.getCities().then(res => this.citiesList = res);
  }

  reloadCities(){
    this.veloService.getCities().then(er => {console.log(er);
      this.citiesList = er;});
    console.log(this.citiesList);
  }


}
