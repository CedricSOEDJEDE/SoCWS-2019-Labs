import { Component, OnInit } from '@angular/core';
import {VeloService} from "../../../services/velo.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Station} from "../../../models/station";

@Component({
  selector: 'app-velib-cities',
  templateUrl: './velib-cities.component.html',
  styleUrls: ['./velib-cities.component.css']
})
export class VelibCitiesComponent implements OnInit {

  stationList: Station[];
  city: string;

  constructor(private veloService: VeloService, private route: ActivatedRoute, private router: Router) {
    this.stationList = [];
    this.route.params.subscribe( params => this.city = params.city);
  }

  ngOnInit() {
    this.veloService.getStation(this.city).then(res => this.stationList = res);
  }

  goToStation(station: string){
    this.router.navigate(['/stations',this.city, station]);
  }

  reloadStation(){
    this.veloService.getStation(this.city).then(er => {console.log(er);
      this.stationList = er;});
    console.log(this.stationList);
  }

  return(){
    this.router.navigate(['/']);
  }

}
