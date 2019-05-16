import { Injectable } from '@angular/core';
import {Client, ISoapMethodResponse, NgxSoapService} from "ngx-soap";
import {Station} from "../models/station";

@Injectable()
export class MonitoringService {

  private readonly client: Promise<Client>;


  constructor(private soap: NgxSoapService) {
    this.client = this.soap.createClient('/Monitoring/?singleWsdl', {}, '/Monitoring/');
  }

  public async getLogs(): Promise<string[]> {
    const client = await this.client;
    const logsResponse = (await ((<any>client).getLogs({}).toPromise())).result.getLogsResult.KeyValueOfdateTimestring;

    let logs: string[] = [];

    for(let i = 0; i<logsResponse.length; i++){
      logs[i] = logsResponse[i].Value;
    }

    return Promise.resolve(logs);
  }

  public async getNumberOfRequest(): Promise<string[]> {
    const client = await this.client;
    const numberRequest = (await ((<any>client).getLogsCount({}).toPromise())).result.getLogsCountResult;

    let logs: string[] = [];

    logs[0] = "Le nombre de requête faite est de: "+numberRequest;

    return Promise.resolve(logs);
  }

}
