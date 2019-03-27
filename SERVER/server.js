// User .env file
require("dotenv").config();

// Use express server
const port = process.env.PORT || 6969;
const express = require("express");
const app = express();

// Use body-parser to extract body of requests
const bodyParser = require("body-parser");
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());

// Use session to save cookie
const expressSession = require("express-session");
app.use(expressSession({secret: "keyboard cat"}));

// User passport to authenticate
const passport = require("passport");
app.use(passport.initialize());
app.use(passport.session());

// Get routes
const routes = require("./routes");
routes(app);

// Start server
app.listen(port);
console.log("SERVER IS RUNNING ON PORT " + port);