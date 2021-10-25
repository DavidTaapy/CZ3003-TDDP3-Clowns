import express from "express";
import { getRes,addRes } from "../controllers/restaurant.controller.js";

const router = express.Router();

router.get("/restaurant", getRes);
router.post("/restaurant", addRes);
//router.post("/dish", addDish);

export {router as resRouter};

