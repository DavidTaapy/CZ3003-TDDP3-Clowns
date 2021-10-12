import express from "express";
import { getQuestions,addQuestions } from "../controllers/question.controller.js";

const router = express.Router();

router.get("/questions", getQuestions);
router.post("/questions", addQuestions)

export {router as questionRouter};

