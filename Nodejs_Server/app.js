const express = require("express");
const app = express();
var { nanoid } = require('nanoid')

// parse requests of content-type: application/json
app.use(express.json());

// parse requests of content-type: application/x-www-form-urlencoded
app.use(express.urlencoded({ extended: true }));

// simple route
app.get("/", (req, res) => {
  res.json({ message: "Welcome to bezkoder application." });
});

let users = "{'userId' : nanoid(),  'username' : 'ryan','primaryLevel' : 2, 'points' : 1000}"


app.get("/", (req, res) => {
  res.json({ message: "Welcome to Food wars." });
});


app.get('/user', (req, res) => {
  res.send(users);
});


require("./routes/user.routes.js")(app);

// set port, listen for requests
app.listen(3000, () => {
  console.log("Server is running on port 3000.");
});