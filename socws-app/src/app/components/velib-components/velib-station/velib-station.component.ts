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
    this.veloService.getStationInformation(this.city, this.stationID).then(res => this.station = res);
  }

  reloadStation(){
    this.veloService.getStationInformation(this.city, this.stationID).then(er => {console.log(er);
      this.station = er;});
    console.log(this.station);
  }

  return(){
    this.router.navigate(['/cities',this.city]);
  }

}
