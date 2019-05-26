"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT * FROM subjects";
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
            let sqlCom = "INSERT INTO subjects SET ?";
            db.query(sqlCom, [data], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Mã môn học đã tồn tại" : "Thêm môn học thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    update: function(req, res) {
        if (req.isAuthenticated()) {
            let data = req.body;
            let subjectid = req.params.subjectid;
            let sqlCom = "UPDATE subjects SET ? WHERE subjectid = ?";
            db.query(sqlCom, [data, subjectid], function(err, response){
                if (err) res.status(203);
                res.send(err ? err : "Sửa môn học thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}