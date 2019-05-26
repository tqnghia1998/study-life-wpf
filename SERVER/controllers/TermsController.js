"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT * FROM terms ORDER BY termyear, termindex";
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
            let sqlCom = "INSERT INTO terms SET ?";
            db.query(sqlCom, [data], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Học kỳ này đã tồn tại" : "Thêm học kỳ thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    update: function(req, res) {
        if (req.isAuthenticated()) {
            let data = req.body;
            let termindex = req.params.termindex;
            let termyear = req.params.termyear;
            let sqlCom = "UPDATE terms SET ? WHERE termindex = ? AND termyear = ?";
            db.query(sqlCom, [data, termindex, termyear], function(err, response){
                if (err) res.status(203);
                res.send(err ? err : "Sửa học kỳ thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}