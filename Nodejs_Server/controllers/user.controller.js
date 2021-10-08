import {firestore} from "../model/db.js";
import { User }  from "../model/user.model.js";

// Create and save a new user
const createUser = async(req, res) => {
    try {
        const data = req.body;
        // TODO: User class is not used here, so we assume the request already has all the required attributes before putting into db?
        const userdb = firestore.collection('users');
        await userdb.doc(String(data.id)).set(data);
        res.send("user added!");
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
        if (!currUser.exists) {
            res.send("No such user!");
        } else {
            res.send(JSON.stringify(currUser.data()));
        }
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
        if (!currUser.exists) {
            res.send("No such user!");
        } else {
            await userdb.doc(String(id)).delete();
            res.send("User is deleted!");
        }
        
    } catch (error) {
        return res.status(400).send("Invalid user Id");
        //res.send("invalid user");
    }
};

// Update a user identified by the userId in the request
const updateUser = async(req, res) => {
    try {
        const data = req.body;
        const userdb = firestore.collection('users');
        const updatedUser = await userdb.doc(String(data.id)).set(data)
        res.send("user is updated!");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("invalid update");
    }

};

export {createUser, getUser, deleteUser, updateUser};