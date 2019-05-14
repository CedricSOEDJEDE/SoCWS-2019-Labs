export class Station {

  id: string;
  name: string;
  numberOfBike;
  numberofStand;

  constructor(id: string,
              name: string,
              numberOfBike,
              numberOfStand) {
    this.id = id;
    this.name = name;
    this.numberOfBike = numberOfBike;
    this.numberofStand = numberOfStand;
  }

}
