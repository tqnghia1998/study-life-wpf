"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT *, s.subjectname FROM tasks t left join subjects s on t.subjectid = s.subjectid WHERE userid = ?";
            db.query(sqlQuery, [req.user.userid], function(err, response){
                console.log(response);
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
            data.userid = req.user.userid;
            let sqlCom = "INSERT INTO tasks SET ?";
            db.query(sqlCom, [data], function(err, response){
                if (err) {
                    res.status(203);
                    console.log(err);
                }
                res.send(err ? "Đã xảy ra lỗi khi tạo bài tập" : "Thêm bài tập thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    update: function(req, res) {
        if (req.isAuthenticated()) {
            let data = req.body;
            let taskid = req.params.taskid;
            if (data.subjectid == '') data.subjectid = null;

            let sqlCom = "UPDATE tasks SET ? WHERE taskid = ? ";
            db.query(sqlCom, [data, taskid], function(err, response){
                if (err) {
                    res.status(203);
                }
                res.send(err ? "Đã xảy ra lỗi khi sửa bài tập" : "Sửa bài tập thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    delete: function(req, res) {
        if (req.isAuthenticated()) {
            let taskid = req.params.taskid;
            let sqlCom = "DELETE FROM tasks WHERE taskid = ? AND userid = ?";
            db.query(sqlCom, [taskid, req.user.userid], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Đã xảy ra lỗi khi xóa bài tập" : "Xóa bài tập thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}