import express from "express";
import { getItems,addItems, deleteItem } from "../controllers/item.controller.js";

const router = express.Router();

router.get("/items", getItems);
router.post("/items", addItems);
router.delete("/items", deleteItem);

export {router as itemRouter};

