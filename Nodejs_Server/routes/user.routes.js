module.exports = app => {
    const users = require("../controllers/user.controller.js");
  
    // Create a new User
    app.post("/user", users.create);
  
    // Retrieve all Users
    //app.get("/user", users.findAll);
  
    // Retrieve a single User with userId
    app.get("/user/:userId", users.findOne);
  
    // Update a User with userId
    app.put("/user/:userId", users.update);
  
    // Delete a user with userId
    app.delete("/user/:userId", users.delete);
  
    // Create a new User
    app.delete("/user", users.deleteAll);
  };