"use strict"
const db = require("./../db");

module.exports = {
    update: function(req, res) {
        if (req.isAuthenticated()) {
            let data = req.body;
            let userid = req.params.userid;
            let sqlCom = "UPDATE users SET ? WHERE userid = ?";
            db.query(sqlCom, [data, userid], function(err, response){
                if (err) res.status(203);
                res.send(err ? err : "Cập nhật thông tin thành công");
            });
        }
        else {
            res.json("Đã hết phiên hoạt động");
        }
    }
}