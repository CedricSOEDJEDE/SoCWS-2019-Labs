import { Injectable } from '@angular/core';
import {Client, ISoapMethodResponse, NgxSoapService} from "ngx-soap";
import {Station} from "../models/station";

@Injectable()
export class VeloService {

  private readonly client: Promise<Client>;
  message;

  constructor(private soap: NgxSoapService) {
    this.client = this.soap.createClient('/Velib/?singleWsdl', {}, '/Velib/');
  }
/*
  public async getCities() {
    const client = await this.client;
    const body = {
      user: "admin"
    };
    const stations_data = (await ((<any>this.client).getContracts(body).toPromise()));
    console.log(stations_data.message);
    return stations_data;
  }*/

  public async getCities(): Promise<string[]> {
    const body = {
      user: "user"
    };
    const client = await this.client;
    return (await ((<any>client).getContracts(body).toPromise())).result.getContractsResult.string;
  }

  public async getStation(city : string): Promise<Station[]> {
    const body = {
      contract: city,
      user: "user"
    };
    const client = await this.client;
    console.log();
    const stationsResponse = (await ((<any>client).getStations(body).toPromise())).result.getStationsResult.ArrayOfstring;
    const listName = stationsResponse[0].string;
    const listId = stationsResponse[1].string;
    const listNumberofBike = stationsResponse[2].string;
    const listNumberofStand = stationsResponse[3].string;

    console.log(stationsResponse);

    let listStation: Station[] = [];

    for (let i = 0; i < listName.length; i++) {
      listStation[i] = new Station(listId[i], listName[i], listNumberofBike[i], listNumberofStand[i]);
    }

    return Promise.resolve(listStation);
   }

}
