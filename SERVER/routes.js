"use strict"
module.exports = function(app) {
    const loginCtrl = require("./controllers/LoginController");
    const facultiesCtrl = require("./controllers/FacultiesController");
    const productsCtrl = require("./controllers/ProductsController");

    /* LOGIN CONTROLLER */
    app.route("/login")
    .post(loginCtrl.login);
    app.route("/login/fail")
    .get(loginCtrl.loginFail);
    app.route("/login/success")
    .get(loginCtrl.loginSuccess);
    app.route("/login/logout")
    .get(loginCtrl.logout);
    app.route("/login/register")
    .post(loginCtrl.register);

    /* FACULTIES CONTROLLER */
    app.route("/faculties")
    .get(facultiesCtrl.get)
    .post(facultiesCtrl.post);
    app.route("/faculties/:facultyid")
    .put(facultiesCtrl.update);

    /* PRODUCTS CONTROLLER */
    app.route("/products")
    .get(productsCtrl.get)
    .post(productsCtrl.store)
    app.route("/products/:productId")
    .get(productsCtrl.detail)
    .put(productsCtrl.update)
    .delete(productsCtrl.delete);
};