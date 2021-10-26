'use strict';
import express, { json, urlencoded } from "express";
import { userRouter } from './routes/user.routes.js';
import { leaderboardRouter } from './routes/leaderboard.routes.js';
import { questionRouter } from './routes/question.routes.js';
import { itemRouter } from './routes/item.routes.js';
import { resRouter } from './routes/restaurant.routes.js';
import { characterRouter } from './routes/character.routes.js';

const app = express();

// parse requests of content-type: application/json
app.use(json());

// parse requests of content-type: application/x-www-form-urlencoded
app.use(urlencoded({ extended: true }));

//let users = "{'userId' : nanoid(),  'username' : 'ryan','primaryLevel' : 2, 'points' : 1000}"


app.get("/", (req, res) => {
  res.json({ message: "Welcome to Food wars." });
});

app.use('/', userRouter);
app.use('/', leaderboardRouter);
app.use('/', questionRouter);
app.use('/', itemRouter);
app.use('/', resRouter);
app.use('/', characterRouter);

// set port, listen for requests
app.listen(3000, () => {
  console.log("Server is running on port 3000.");
});

export {app as server};