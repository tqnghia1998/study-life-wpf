"use strict"
const db = require("./../db");

module.exports = {
    termyear: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT distinct termyear FROM terms";
            db.query(sqlQuery, function(err, response){
                res.send(err ? "Không thể kết nối đến dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    termindex: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT distinct termindex FROM terms where termyear = ?";
            db.query(sqlQuery, [req.params.termyear], function(err, response){
                console.log(response);
                res.send(err ? "Không thể kết nối đến dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },

    unregisted: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT *, s.subjectid FROM subjects s join terms t on s.termindex = t.termindex and s.termyear = t.termyear left join registers r on s.subjectid = r.subjectid where s.termyear = ? and s.termindex = ? and (userid <> ? or userid is null)";
            db.query(sqlQuery, [req.params.termyear, req.params.termindex, req.user.userid], function(err, response){
                console.log(response);
                res.send(err ? "Không thể kết nối đến dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },

    registed: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT *, s.subjectid FROM subjects s join terms t on s.termindex = t.termindex and s.termyear = t.termyear left join registers r on s.subjectid = r.subjectid where s.termyear = ? and s.termindex = ? and userid = ?";
            db.query(sqlQuery, [req.params.termyear, req.params.termindex, req.user.userid], function(err, response){
                console.log(response);
                res.send(err ? "Không thể kết nối đến dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },

    // registed: function(req, res) {
    //     if (req.isAuthenticated()) {
    //         const sqlQuery = "SELECT * FROM registers r join subjects s on r.subjectid = s.subjectid where userid = ?";
    //         db.query(sqlQuery, [req.user.userid], function(err, response){
    //             console.log(response);
    //             res.send(err ? "Không thể kết nối đến dữ liệu" : response);
    //         });
    //     }
    //     else {
    //         res.json("Đã hết phiên hoạt động");
    //     }
    // },

    post: function(req, res) {
        if (req.isAuthenticated()) {
            let data = req.body;
            data.userid = req.user.userid;
            let sqlCom = "INSERT INTO registers SET ?";
            db.query(sqlCom, [data], function(err, response){
                if (err) {
                    console.log(err);
                    res.status(203);
                }
                res.send(err ? "Đã xảy ra lỗi khi tạo bài tập" : "Đăng ký môn học thành công");
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
            let subjectid = req.body.subjectid;
            let sqlCom = "DELETE FROM registers WHERE subjectid = ? AND userid = ?";
            console.log(sqlCom);
            console.log(subjectid);
            console.log(req.user.userid);
            db.query(sqlCom, [subjectid, req.user.userid], function(err, response){
                if (err) res.status(203);
                res.send(err ? "Đã xảy ra lỗi khi huỷ đăng ký" : "Huỷ đăng ký môn học thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}