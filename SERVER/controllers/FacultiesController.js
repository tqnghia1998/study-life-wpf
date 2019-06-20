"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        if (true) {
            const sqlQuery = "SELECT * FROM faculties";
            db.query(sqlQuery, function(err, response){
                res.send(err ? "Không thể kết nối đến dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    post: function(req, res) {
        if (req.isAuthenticated()) {
            let data = req.body;
            let sqlCom = "INSERT INTO faculties SET ?";
            db.query(sqlCom, [data], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Mã khoa đã tồn tại" : "Thêm khoa thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    update: function(req, res) {
        if (req.isAuthenticated()) {
            let data = req.body;
            let facultyid = req.params.facultyid;
            let sqlCom = "UPDATE faculties SET ? WHERE facultyid = ?";
            db.query(sqlCom, [data, facultyid], function(err, response){
                if (err) res.status(203);
                res.send(err ? err : "Sửa khoa thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    getByUserId: function(req, res) {
        if (req.isAuthenticated()) {
            let userid = req.params.userid;
            let sqlCom = "SELECT facultyname FROM users LEFT JOIN faculties ON users.faculty = faculties.facultyid WHERE users.userid = ?";
            db.query(sqlCom, [userid], function(err, response){
                if (err) res.status(203);
                res.send(err ? err : response[0].facultyname);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}