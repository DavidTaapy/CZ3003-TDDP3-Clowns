import express from "express";
import { getLeaderboard } from "../controllers/leaderboard.controller.js";

const router = express.Router();
// Create a new User
router.get("/leaderboard", getLeaderboard);

export {router as leaderboardRouter};

