import express from "express";
import { getCharacter,addCharacter, deleteCharacter } from "../controllers/character.controller.js";

const router = express.Router();

router.get("/character", getCharacter);
router.post("/character", addCharacter);
router.delete("/character", deleteCharacter);

export {router as characterRouter};