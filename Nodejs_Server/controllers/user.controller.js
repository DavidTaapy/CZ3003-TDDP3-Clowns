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
    console.log("Hi create");
};

// Retrieve and return a user
const getUser = async(req, res) => {
    try {
        const data = req.query.id;
        const userdb = firestore.collection('users');
        const currUser = await userdb.doc(String(data)).get();
        res.send(currUser);
    } catch (error) {
        return res.status(400).send(error.message);
    }
    console.log("Hi get");
};

// Delete a user with the specified userId in the request
const deleteUser = async(req, res) => {
    try {
        const id = req.params;
        const userdb = firestore.collection('users');
        const deletedUser = await userdb.where("id", "==", id).delete();
        res.send(deletedUser.userName + "is deleted!");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("invalid user");
    }
    console.log("Hi delete");  
};

// Update a user identified by the userId in the request
const updateUser = async(req, res) => {
    try {
        const data = req.body;
        const userdb = firestore.collection('users');
        const updatedUser = await userdb.where("id", "==", data.id).set(data)
        res.send(updatedUser.userName + "is updated!");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("invalid transaction");
    }
    console.log("Hi update");  
};
// Retrieve all Customers from the database.
/* Leave blank first
exports.findAll = (req, res) => {
  
};

// Find a single Customer with a customerId
export function findOne(req, res) {
    console.log("Hi findone");
}

// Update a Customer identified by the customerId in the request
export function update(req, res) {
    console.log("Hi update");
}

*/

export {createUser, getUser, deleteUser, updateUser};