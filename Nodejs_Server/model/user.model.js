//const sql = require("./db.js");

// constructor
const User = function(user) {
    // TODO: fill up the user stuff you gonna take out from database
    
    //this.email = user.email;
    //this.name = user.name;
    this.active = user.active;
  };

// TODO: fill in all methods
User.create = (newUser, result) => {
    sql.query();
};


// Find a single Customer with a customerId
User.findOne = (userId, result) => {
    sql.query();
};

// Update a Customer identified by the customerId in the request
User.update = (userId, user, result) => {
    sql.query();
};

// Delete a Customer with the specified customerId in the request
User.delete = (userId, result) => {
    sql.query();
};