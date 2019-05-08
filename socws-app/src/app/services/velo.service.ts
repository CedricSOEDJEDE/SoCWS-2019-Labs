import { Injectable } from '@angular/core';
import {Client, ISoapMethodResponse, NgxSoapService} from "ngx-soap";

@Injectable()
export class VeloService {
  client: Client;

  constructor(private soap: NgxSoapService) {
    this.soap.createClient('http://localhost:8732/IWS/JCDECAUX_SOAP/?wsdl').then(client => this.client = client).catch(er => console.log("Error"));
  }

  getContracts() {
      (this.client as any).getContracts().subscribe((res: ISoapMethodResponse) => console.log("result"));
  }
}
