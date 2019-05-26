"use strict"
module.exports = function(app) {
    const loginCtrl = require("./controllers/LoginController");
    const facultiesCtrl = require("./controllers/FacultiesController");
    const productsCtrl = require("./controllers/ProductsController");
    const termsCtrl = require("./controllers/TermsController");
    const subjectsCtrl = require("./controllers/SubjectsController");
    const schedulesCtrl = require("./controllers/SchedulesController");


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

    /* TERMS CONTROLLER */
    app.route("/terms")
    .get(termsCtrl.get)
    .post(termsCtrl.post);
    app.route("/terms/:termindex/:termyear")
    .put(termsCtrl.update);

    /* SUBJECT CONTROLLER */
    app.route("/subjects")
    .get(subjectsCtrl.get)
    .post(subjectsCtrl.post);
    app.route("/subjects/:subjectid")
    .put(subjectsCtrl.update);
    
    /* SCHEDULE CONTROLLER */
    app.route("/schedules/:subjectid")
    .get(schedulesCtrl.get);
    app.route("/schedules")
    .post(schedulesCtrl.post);
    app.route("/schedules/:subjectid/:day")
    .put(schedulesCtrl.update)
    .delete(schedulesCtrl.delete);

    /* PRODUCTS CONTROLLER */
    app.route("/products")
    .get(productsCtrl.get)
    .post(productsCtrl.store)
    app.route("/products/:productId")
    .get(productsCtrl.detail)
    .put(productsCtrl.update)
    .delete(productsCtrl.delete);
};