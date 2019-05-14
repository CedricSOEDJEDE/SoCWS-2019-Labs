import { Component, OnInit } from '@angular/core';
import {VeloService} from "../../../services/velo.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Station} from "../../../models/station";

@Component({
  selector: 'app-velib-station',
  templateUrl: './velib-station.component.html',
  styleUrls: ['./velib-station.component.css']
})
export class VelibStationComponent implements OnInit {

  city: string;
  stationID: string;
  station: Station;

  constructor(private veloService: VeloService, private route: ActivatedRoute, private router: Router) {
    this.route.params.subscribe( params => {
      this.city = params.city;
      this.stationID = params.station;
    });
  }
  ngOnInit() {
  }

  reloadStation(){
    /*this.veloService.getStation(this.city).then(er => {console.log(er);
      this.stationList = er;});
    console.log(this.stationList);*/
  }

  return(){
    this.router.navigate(['/cities',this.city]);
  }

}
