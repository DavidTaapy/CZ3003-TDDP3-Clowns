'use strict';
import express, { json, urlencoded } from "express";
import { userRouter } from './routes/user.routes.js';
import { leaderboardRouter } from './routes/leaderboard.routes.js';

const app = express();

// parse requests of content-type: application/json
app.use(json());

// parse requests of content-type: application/x-www-form-urlencoded
app.use(urlencoded({ extended: true }));

app.get("/", (req, res) => {
  res.json({ message: "Welcome to Food wars." });
});

app.use('/', userRouter);
app.use('/', leaderboardRouter);

// set port, listen for requests
app.listen(3000, () => {
  console.log("Server is running on port 3000.");
});