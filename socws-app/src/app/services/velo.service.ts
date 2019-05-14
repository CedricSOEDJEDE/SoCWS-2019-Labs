import { Injectable } from '@angular/core';
import {Client, ISoapMethodResponse, NgxSoapService} from "ngx-soap";

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
    //(<any>this.client).getContracts(body).subscribe((res: ISoapMethodResponse) => this.message = res.result.getContractsResult);
    //return this.message;
  }

}
