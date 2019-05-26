"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        if (req.isAuthenticated()) {
            let subjectid = req.params.subjectid;
            const sqlQuery = "SELECT * FROM schedules WHERE subjectid = ?";
            db.query(sqlQuery, [subjectid], function(err, response){
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
            let sqlCom = "INSERT INTO schedules SET ?";
            db.query(sqlCom, [data], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Đã xảy ra lỗi khi tạo lịch học" : "Thêm lịch học thành công");
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
            let day = req.params.day;
            let sqlCom = "UPDATE schedules SET ? WHERE subjectid = ? AND day = ?";
            db.query(sqlCom, [data, subjectid, day], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Đã xảy ra lỗi khi sửa lịch học" : "Sửa lịch học thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    delete: function(req, res) {
        if (req.isAuthenticated()) {
            let subjectid = req.params.subjectid;
            let day = req.params.day;
            let sqlCom = "DELETE FROM schedules WHERE subjectid = ? AND day = ?";
            db.query(sqlCom, [subjectid, day], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Đã xảy ra lỗi khi xóa lịch học" : "Xóa lịch học thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}