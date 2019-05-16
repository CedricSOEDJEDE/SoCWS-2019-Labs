import { Component, OnInit } from '@angular/core';
import {MonitoringService} from "../../services/monitoring.service";

@Component({
  selector: 'app-monitoring',
  templateUrl: './monitoring.component.html',
  styleUrls: ['./monitoring.component.css']
})
export class MonitoringComponent implements OnInit {

  logs: string[];

  constructor(private monitoringService: MonitoringService) {
    this.logs = [];
  }

  getLog(){
    this.monitoringService.getLogs().then( resp => this.logs = resp);
  }

  getNumberOfRequest(){
    this.monitoringService.getNumberOfRequest().then( resp => this.logs = resp);
  }

  ngOnInit() {
  }

}
