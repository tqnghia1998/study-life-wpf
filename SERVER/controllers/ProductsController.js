"use strict"
const db = require("./../db");

module.exports = {
    get: function(req, res) {
        if (req.isAuthenticated()) {
            const sqlQuery = "SELECT * FROM products";
            db.query(sqlQuery, function(err, response){
                if (err) throw err;
                res.json(response);
            });
        } else {
            res.json("Unauthenticated");
        }
    },
    detail: function(req, res) {
        let sqlQuery = "SELECT * FROM products WHERE ID = ?";
        db.query(sqlQuery, [req.params.productId], function(err, response) {
            if (err) throw err;
            res.json(response);
        });
    },
    update: function(req, res) {
        let data = req.body;
        let productId = req.params.productId;
        let sqlCom = "UPDATE PRODUCTS SET ? WHERE ID = ?";
        db.query(sqlCom, [data, productId], function(err, response){
            if (err) res.json({
                message: data
            });
            res.json({
                message: "Update success!"
            });
        });
    },
    store: function(req, res) {
        let data = req.body;
        let sqlCom = "INSERT INTO PRODUCTS SET ?";
        db.query(sqlCom, [data], function(err, response){
            res.json({
                message: err ? "Mã sản phẩm đã tồn tại!": "Insert success!"
            });
        });
    },
    delete: (req, res) => {
        let sqlCom = "DELETE FROM PRODUCTS WHERE ID = ?"
        db.query(sqlCom, [req.params.productId], (err, response) => {
            if (err) throw err;
            res.json({
                message: "Delete success!"
            });
        });
    }
}