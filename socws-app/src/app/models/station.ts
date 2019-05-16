export class Station {

  id: string;
  name: string;
  numberOfBike;
  numberofStand;
  adress: string;

  constructor(id: string,
              name: string,
              numberOfBike,
              numberOfStand,
              adresss: string) {
    this.id = id;
    this.name = name;
    this.numberOfBike = numberOfBike;
    this.numberofStand = numberOfStand;
    this.adress = adresss;
  }

}
