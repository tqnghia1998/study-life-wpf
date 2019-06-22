"use strict"
module.exports = function(app) {
    const loginCtrl = require("./controllers/LoginController");
    const facultiesCtrl = require("./controllers/FacultiesController");
    const termsCtrl = require("./controllers/TermsController");
    const subjectsCtrl = require("./controllers/SubjectsController");
    const schedulesCtrl = require("./controllers/SchedulesController");
    const tasksCtrl = require("./controllers/TaskController");
    const registerCtrl = require("./controllers/RegisterController");
    const registersCtrl = require("./controllers/RegistersController");
    const usersCtrl = require("./controllers/UsersController");
    const statisticsCtrl = require("./controllers/StatisticsController");


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
    app.route("/faculties/:userid")
    .get(facultiesCtrl.getByUserId);

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
    .put(subjectsCtrl.update)
    
    /* SCHEDULE CONTROLLER */
    app.route("/schedules/:subjectid")
    .get(schedulesCtrl.get);
    app.route("/schedules")
    .post(schedulesCtrl.post);
    app.route("/schedules/:subjectid/:day")
    .put(schedulesCtrl.update)
    .delete(schedulesCtrl.delete);

    // TASKS CONTROLLER
    app.route("/tasks")
    .get(tasksCtrl.get)
    .post(tasksCtrl.post)
    app.route("/tasks/:taskid")
    .put(tasksCtrl.update)
    
    // REGISTERS CONTROLLER
    app.route("/register")
    .get(registerCtrl.termyear)
    .post(registerCtrl.post)
    .delete(registerCtrl.delete)
    app.route("/register/:termyear")
    .get(registerCtrl.termindex)
    app.route("/register/:termyear/:termindex")
    .get(registerCtrl.unregisted)
    app.route("/registed/:termyear/:termindex")
    .get(registerCtrl.registed)
    /* REGISTERS CONTROLLER */
    app.route("/registers/:userid")
    .get(registersCtrl.getByUserId);
    app.route("/registers/:userid/:subjectid")
    .delete(registersCtrl.deregisterByUserId);
    app.route("/registers/registered/:userid")
    .get(registersCtrl.getSubjectRegistered);
    app.route("/registers/notregistered/:userid")
    .get(registersCtrl.getSubjectNotRegistered);

    /* USERS CONTROLLER */
    app.route("/users/:userid")
    .put(usersCtrl.update);

    /* STATISTICS CONTROLLER */
    app.route("/statistics/subjectdensities")
    .get(statisticsCtrl.getSubjectDensities);
    app.route("/statistics/registernumbers/:facultyid")
    .get(statisticsCtrl.getRegisterNumbers);
};