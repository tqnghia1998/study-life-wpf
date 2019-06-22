"use strict"
const db = require("./../db");

module.exports = {
    getSubjectDensities: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT faculties.facultyid AS Id, faculties.facultyname AS Name, COUNT(subjects.subjectid) as Amount "
                                + "FROM faculties LEFT JOIN subjects ON "
                                + "subjects.faculty = faculties.facultyid GROUP BY facultyid";
            db.query(sqlQuery, function(err, response){
                res.send(err ? "Không thể kết nối tới dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    },
    getRegisterNumbers: function(req, res) {
        if (true) {
            let facultyid = req.params.facultyid;
            const sqlQuery = "SELECT subjects.subjectid AS Id, subjects.subjectname AS Name, COUNT(*) as Amount "
                                + "FROM subjects INNER JOIN registers ON "
                                + "subjects.subjectid = registers.subjectid WHERE subjects.faculty = ? "
                                + "GROUP BY registers.subjectid";
            db.query(sqlQuery, facultyid, function(err, response){
                res.send(err ? "Không thể kết nối tới dữ liệu" : response);
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}