import express from "express";
import { getLeaderboard, getPastLeaderboard } from "../controllers/leaderboard.controller.js";

const router = express.Router();

router.get("/leaderboard", getLeaderboard);
router.get("/pastleaderboard", getPastLeaderboard);

export {router as leaderboardRouter};

