"use strict"
const db = require("./../db");
const passport = require("passport");
const localStrategy = require("passport-local").Strategy;

// Check if the userID and password are valid
passport.use(new localStrategy(
    function(userid, password, done) {
        const sqlQuery = "SELECT * FROM users WHERE userid = ? AND password = ?";
        db.query(sqlQuery, [userid, password], function(err, resp) {
            return done(null, resp.length > 0 ? resp[0] : false);
        });
    }
))

// When authentication succeed
passport.serializeUser(function(userInfo, done) {
    return done(null, userInfo);
})
passport.deserializeUser(function(userInfo, done) {
    return done(null, userInfo);
})

// Export routes
module.exports = {
    login: passport.authenticate("local", {
        failureRedirect: "/login/fail",
        successRedirect: "/login/success"
    }),
    loginFail: function(req, res) {
        res.status(203);
        res.send("Thông tin đăng nhập sai");
    },
    loginSuccess: function(req, res) {
        res.send(req.user);
    },
    logout: function(req, res) {
        req.session.destroy();
        res.json("Logged out");
    },
    register: function(req, res) {
        var data = req.body;
        var sqlC = "INSERT INTO users SET ?";
        db.query(sqlC, [data], function(err, response) {
            if (err) res.status(203);
            res.send(err ? "Tài khoản đã tồn tại" : "Đăng ký thành công");
        })
    }
}