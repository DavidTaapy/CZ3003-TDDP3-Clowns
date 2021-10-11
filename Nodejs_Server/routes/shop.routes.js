import express from "express";
import { getItems,addItems } from "../controllers/shop.controller.js";

const router = express.Router();

router.get("/shop", getItems);
router.post("/shop", addItems)

export {router as shopRouter};

