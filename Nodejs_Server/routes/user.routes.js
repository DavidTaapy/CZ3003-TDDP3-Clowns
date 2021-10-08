import express from "express";
import { createUser, getUser, deleteUser, updateUser } from "../controllers/user.controller.js";

const router = express.Router();
// Create a new User
router.post("/user", createUser);
router.get("/user", getUser);
router.delete("/user", deleteUser);
router.put("/user", updateUser);

export {router as userRouter};

