"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        const sqlQuery = "SELECT * FROM faculties";
            db.query(sqlQuery, function(err, response){
                if (err) throw err;
                res.json(response);
            });
    }
}