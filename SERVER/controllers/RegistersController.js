"use strict"
const db = require("./../db");

module.exports = {
    getByUserId: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT registers.userid, registers.status, subjects.*, schedules.day, schedules.room, schedules.starttime, schedules.finishtime, "
                             + "faculties.facultyname, terms.begindate, terms.enddate FROM registers "
                             + "INNER JOIN subjects ON registers.subjectid = subjects.subjectid "
                             + "LEFT JOIN schedules ON subjects.subjectid = schedules.subjectid "
                             + "LEFT JOIN faculties ON subjects.faculty = faculties.facultyid "
                             + "LEFT JOIN terms ON subjects.termindex = terms.termindex AND subjects.termyear = terms.termyear "
                             + "WHERE registers.userid = ?";
            let userid = req.params.userid;
            db.query(sqlQuery, userid, function(err, response){
                res.send(err ? "Không thể kết nối đến dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    deregisterByUserId: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "DELETE FROM registers WHERE userid = ? AND subjectid = ?";
            let userid = req.params.userid;
            let subjectid = req.params.subjectid;
            db.query(sqlQuery, [userid, subjectid], function(err, response){
                res.send(err ? "Không thể kết nối đến dữ liệu" : "Hủy đăng ký thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    
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

    subject: function(req, res) {
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
}