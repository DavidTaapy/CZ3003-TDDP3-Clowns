'use strict';
import express, { json, urlencoded } from "express";
import { router } from './routes/user.routes.js';
const app = express();

// parse requests of content-type: application/json
app.use(json());

// parse requests of content-type: application/x-www-form-urlencoded
app.use(urlencoded({ extended: true }));


// simple route
app.get("/", (req, res) => {
  res.json({ message: "Welcome to bezkoder application." });
});


app.use('/', router);

// set port, listen for requests
app.listen(3000, () => {
  console.log("Server is running on port 3000.");
});