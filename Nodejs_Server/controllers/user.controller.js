import {firestore} from "../model/db.js";
import { User }  from "../model/user.model.js";

// Create and save a new user
const createUser = async(req, res) => {
    try {
        const data = req.body;
        if (data.userName == null || data.primaryLevel == null) {res.send("please fill up all fields!");}
        else {
            const newUser = new User(data.userName, data.primaryLevel);
            const userdb = firestore.collection('users');
            userdb.add(JSON.parse(JSON.stringify(newUser)))
            .then(function(docRef) {
                userdb.doc(docRef.id).set({"id": docRef.id}, { merge: true });
            });
            res.send("user added!");
        }
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding user!");
    }
};

// Retrieve and return a user
const getUser = async(req, res) => {
    try {
        const id = req.query.id;
        const userdb = firestore.collection('users');
        const currUser = await userdb.doc(String(id)).get();
        res.send(JSON.stringify(currUser.data()));
    } catch (error) {
        res.status(400).send(error.message);
        res.send("user doesnt exist!");
    }
};

// Delete a user with the specified userId in the request
const deleteUser = async(req, res) => {
    try {
        const id = req.query.id;
        const userdb = firestore.collection('users');
        const currUser = await userdb.doc(String(id)).get();
        if (!currUser.exists) {res.send("no such user!");}
        else {
            await userdb.doc(String(id)).delete();
            res.send("user is deleted!");
        }
        
    } catch (error) {
        return res.status(400).send("invalid");
        //res.send("invalid user");
    }
};

// Update a user identified by the userId in the request body
const updateUser = async(req, res) => {
    try {
        const data = req.body;
        const id = data.id;
        const userdb = firestore.collection('users');
        const currUser = await userdb.doc(String(id)).get();
        if (!currUser.exists) {res.send("no such user!");}
        else { const updatedUser = await userdb.doc(String(data.id)).set(data)
            res.send("user is updated!");}
    } catch (error) {
        res.status(400).send(error.message);
        res.send("invalid update");
    } 

};

export {createUser, getUser, deleteUser, updateUser};