import {firestore} from "../model/db.js";
import { User }  from "../model/user.model.js";
// Create and Save a new Customer

const createUser = async(req, res, next) => {
    try {
        const data = req.body;
        // TODO: User class is not used here, so we assume the request already has all the required attributes before putting into db?
        await firestore.collection('users').add(data);
        res.send("user added!");
    } catch (error) {
        res.status(400).send(error.message);
    }
    console.log("Hi create");
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

// Delete a Customer with the specified customerId in the request
const delete = (req, res) => {
    console.log("Hi delete");
};*/

export {createUser};