import { Injectable } from '@angular/core';
import {firestore} from 'firebase';
import {AngularFirestore} from 'angularfire2/firestore';
import {AngularFireStorage} from 'angularfire2/storage';
import {User} from '../models/user';
import UserCredential = firebase.auth.UserCredential;
import * as firebase from 'firebase';
import {BehaviorSubject} from "rxjs";
import {UserWatcher} from "../models/user-watcher";

@Injectable()
export class UserService {

  constructor(private st: AngularFireStorage, private db: AngularFirestore) {
    this.loggedUser = new UserWatcher();
    firebase.auth().onAuthStateChanged((user) => {
      if (user) {
        this.getLoggedUserFromCache();
      } else {
        this.loggedUser.updateUser(null);
      }
    });}

  loggedUser: UserWatcher;

  public registerUser(user: User): Promise<UserCredential> {
    return firebase.auth().createUserWithEmailAndPassword(user.mail, user.password)
      .then(res => {
        // register user in database when registered on firebase
        this.db.collection('Users').doc(res.user.uid).ref.set({
          mail: user.mail,
          firstName: user.firstName,
          lastName: user.lastName,
          username: user.username,
          role: user.role
        });
        return res;
      });
  }

  public getLoggedFirebaseUser(): firebase.User {
    return firebase.auth().currentUser;
  }

  private getLoggedUserFromCache() {
    console.log('retrieve from cache');
    const docRef = this.db.collection('Users').doc(this.getLoggedFirebaseUser().uid).ref;
    docRef.get().then((doc) => {
      const user = User.createUser(doc.data());
      user.supplyWithFirebaseUser(this.getLoggedFirebaseUser());
      this.loggedUser.updateUser(user);
      this.updateLastConnection(user.userId);
    });
  }

  updateRole(user: User, role: string) {
    return this.db.collection('Users').doc(user.userId).update({
      'role': role
    });
  }

  private updateLastConnection(userId: string) {
    const timestamp = firebase.firestore.FieldValue.serverTimestamp();
    this.db.collection('Users').doc(userId).update({
      lastConnection: timestamp,
    }).catch(error => {
      console.error('From userService.updateLastConnection : ' + error.toString());
    });
  }

  disconnectUser() {
    firebase.auth().signOut().then(() => {
      this.loggedUser.updateUser(null);
    });
  }

  public async getLoggedUser(): Promise<User> {
    return this.loggedUser.getUser();
  }

  public async streamLoggedUser(): Promise<BehaviorSubject<User>> {
    return await this.loggedUser.streamUser();
  }

  loginUser(user: User): Promise<void> {
    return firebase.auth().setPersistence(firebase.auth.Auth.Persistence.LOCAL)
      .then(() => {
        return firebase.auth().signInWithEmailAndPassword(user.mail, user.password).then(() => {
          return this.db.collection('Users').doc(firebase.auth().currentUser.uid).ref.get().then((doc) => {
            const user = User.createUser(doc.data());
            user.supplyWithFirebaseUser(firebase.auth().currentUser);
            this.loggedUser.updateUser(user);
            }
          );
        }).catch(() => {
          console.error('Error while connecting');
        });
      });
  }

}
