"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        const sqlQuery = "SELECT * FROM faculties";
            db.query(sqlQuery, function(err, response){
                res.send(err ? "Không thể kết nối đến dữ liệu" : response);
            });
    },
    post: function(req, res) {
        let data = req.body;
        let sqlCom = "INSERT INTO faculties SET ?";
        db.query(sqlCom, [data], function(err, response){
            if (err) res.status(203);
            res.send(err ? "Mã khoa đã tồn tại" : "Thêm khoa thành công");
        });
    },
    update: function(req, res) {
        let data = req.body;
        let facultyid = req.params.facultyid;
        let sqlCom = "UPDATE faculties SET ? WHERE facultyid = ?";
        db.query(sqlCom, [data, facultyid], function(err, response){
            if (err) res.status(203);
            res.send(err ? err : "Sửa khoa thành công");
        });
    }
}