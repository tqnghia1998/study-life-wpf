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
    }
}