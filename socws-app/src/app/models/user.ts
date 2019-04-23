export class User {

  private userId: string;
  private firstName: string;
  private lastName: string;
  private mail: string;
  private password: string;

  /**
   * @param firstName
   * @param lastName
   * @param mail
   * @param userId
   */
  constructor(firstName: string,
              lastName: string,
              mail: string,
              userId: string) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.mail = mail;
    this.userId = userId;
  }

  /*
  static hasValidMail(user: User): boolean {
    return user.mail.includes('@', 0);
  }

  static hasValidPassword(user: User): boolean {
    return user.password.length >= 5;
  }

  static hasValidFirstAndLastName(user: User): boolean {
    return user.firstName.length > 2 && user.lastName.length > 2;
  }*/
}
