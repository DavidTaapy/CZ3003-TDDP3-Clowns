import express from "express";
import { getItems,addItems } from "../controllers/item.controller.js";

const router = express.Router();

router.get("/items", getItems);
router.post("/items", addItems)

export {router as itemRouter};

