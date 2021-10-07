import { initializeApp } from 'firebase/app';
import { getFirestore, collection, getDocs } from 'firebase/firestore/lite';
import {config} from '../configs/db.configs.js';
import 'firebase/firestore';
import { createRequire } from "module";
import admin from 'firebase-admin';

const require = createRequire(import.meta.url);
const serviceAccount = require('../configs/serviceAccountKey.json');

admin.initializeApp({
  credential: admin.credential.cert(serviceAccount)
});

const firestore = admin.firestore();

export {firestore};