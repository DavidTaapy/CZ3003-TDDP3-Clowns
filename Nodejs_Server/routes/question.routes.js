import express from "express";
import { getQuestions,addQuestions, deleteQuestion } from "../controllers/question.controller.js";

const router = express.Router();

router.get("/questions", getQuestions);
router.post("/questions", addQuestions);
router.delete("/questions", deleteQuestion);

export {router as questionRouter};

