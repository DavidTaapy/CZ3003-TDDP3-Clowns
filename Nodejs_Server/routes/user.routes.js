import express from "express";
import { createUser } from "../controllers/user.controller.js";

const router = express.Router();
// Create a new User
router.post("/user", createUser);

export {router};

// Retrieve all Users
//app.get("/user", users.findAll);

// Retrieve a single User with userId
//app.get("/user/:userId", users.findOne);

// Update a User with userId
//app.put("/user/:userId", users.update);

// Delete a user with userId
//app.delete("/user/:userId", users.delete);

/*module.exports = {
    userRoutes: router
}*/


   