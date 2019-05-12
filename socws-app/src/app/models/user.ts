export class User {

  private _userId: string;
  private _firstName: string;
  private _lastName: string;
  private _mail: string;
  private _password: string;
  private _username: string;

  /**
   * @param firstName
   * @param lastName
   * @param mail
   * @param username
   * @param userId
   */
  constructor(firstName: string,
              lastName: string,
              mail: string,
              username: string,
              userId: string) {
    this._firstName = firstName;
    this._lastName = lastName;
    this._mail = mail;
    this._username = username;
    this._userId = userId;
    this._password = '';
  }

  get username(): string{
    return this._username;
  }

  set username(value: string) {
    this._username = value;
  }

  get firstName(): string {
    return this._firstName;
  }

  set firstName(value: string) {
    this._firstName = value;
  }

  get lastName(): string {
    return this._firstName;
  }

  set lastName(value: string) {
    this._lastName = value;
  }

  get mail(): string {
    return this._mail;
  }

  set mail(value: string) {
    this._mail = value.trim();
  }

  get password(): string {
    return this._password;
  }

  set password(value: string) {
    this._password = value;
  }

  get userId(): string {
    return this._userId;
  }

  set userId(value: string) {
    this._userId = value;
  }

  static createUser(doc){
    return new User(doc.firstName, doc.lastName, doc.mail, doc.username, '');
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
