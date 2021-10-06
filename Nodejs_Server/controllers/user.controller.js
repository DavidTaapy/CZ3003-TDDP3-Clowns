const User = require("../model/user.model.js");

// Create and Save a new Customer
exports.create = (req, res) => {
  console.log("Hi create");
};

// Retrieve all Customers from the database.
/* Leave blank first
exports.findAll = (req, res) => {
  
};*/

// Find a single Customer with a customerId
exports.findOne = (req, res) => {
    console.log("Hi findone");
};

// Update a Customer identified by the customerId in the request
exports.update = (req, res) => {
    console.log("Hi update");
};

// Delete a Customer with the specified customerId in the request
exports.delete = (req, res) => {
    console.log("Hi delete");
};
