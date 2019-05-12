import { Injectable } from '@angular/core';
import {firestore} from 'firebase';
import {AngularFirestore} from 'angularfire2/firestore';
import {AngularFireStorage} from 'angularfire2/storage';
import {User} from '../models/user';
import UserCredential = firebase.auth.UserCredential;
import * as firebase from 'firebase';

@Injectable()
export class UserService {

  constructor(private st: AngularFireStorage, private db: AngularFirestore) {}

  loggedUser: User;

  public registerUser(user: User): Promise<UserCredential> {
    return firebase.auth().createUserWithEmailAndPassword(user.mail, user.password)
      .then(res => {
        // register user in database when registered on firebase
        this.db.collection('Users').doc(res.user.uid).ref.set({
          mail: user.mail,
          firstName: user.firstName,
          lastName: user.lastName,
          username: user.username
        });
        return res;
      });
  }

  disconnectUser() {
    firebase.auth().signOut().then(() => {
      this.loggedUser = null;
    });
  }

  public getLoggedUser(): User {
    console.log(this.loggedUser);
    return this.loggedUser;
  }

  loginUser(user: User): Promise<void> {
    return firebase.auth().setPersistence(firebase.auth.Auth.Persistence.LOCAL)
      .then(() => {
        return firebase.auth().signInWithEmailAndPassword(user.mail, user.password).then(() => {
          return this.db.collection('Users').doc(firebase.auth().currentUser.uid).ref.get().then((doc) => {
              this.loggedUser = User.createUser(doc.data());
            }
          );
        }).catch(() => {
          console.error('Error while connecting');
        });
      });
  }

}
