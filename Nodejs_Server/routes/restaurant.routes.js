import express from "express";
import { getRes,addRes } from "../controllers/restaurant.controller.js";

const router = express.Router();

router.get("/restaurant", getRes);
router.post("/restaurant", addRes);

export {router as resRouter};

