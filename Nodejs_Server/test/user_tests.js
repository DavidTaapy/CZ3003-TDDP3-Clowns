import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

describe('Init', function () {
    it('check app status', function (done) {
      chai.request(server).get('/').end((err, res) => {
        expect(res).to.have.status(200);
        done();
      })
    });
     
});

describe('Testing User API (user.controller.js)', () => {

    var user_id = "testttt";
    var wrong_id = "wrongg_id";
    var new_user = {
        "id": "1234",
        "userName": "ryan",
        "primaryLevel": 1
    }

    var valid_changed_user = {
        "id": "1234",
        "userName": "ryan",
        "primaryLevel": 4
    }

    var invalid_changed_user = {
        "id": "12345",
        "userName": "ryan",
        "primaryLevel": 4
    }
    var id_to_delete = "1234";

    describe('GET user with a valid user_id param', () => {
        it("This should get the user with the corresponding id", (done) => {
            chai.request(server)
            .get("/user")
            .query({id: user_id})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body).to.have.property('id');
                expect(res.body).to.have.property('userName');
                expect(res.body).to.have.property('primaryLevel');
                expect(res.body).to.have.property('id').eq(user_id);
            done();
            })
        })
    })

    describe('GET user with a invalid user_id param', () => {
        it("This should return error message", (done) => {
            chai.request(server)
            .get("/user")
            .query({id: wrong_id})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("user doesnt exist!");
            done();
            })
        })
    })
    
    describe('POST new user into the database', () => {
        it("This should add a user", (done) => {
            chai.request(server)
            .post("/user")
            .type("JSON")
            .send(new_user)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("user added!");
            done();
            })
        })
    })

    describe('PUT update existing user in the database with user_id param', () => {
        it("This should edit an existing user details", (done) => {
            chai.request(server)
            .put("/user")
            .query({id: id_to_delete})
            .type("JSON")
            .send(valid_changed_user)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("user is updated!");
            done();
            })
        })
    })

    describe('PUT update non-existent user in the database with invalid user_id param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .put("/user")
            .send(invalid_changed_user)
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("no such user!");
            done();
            })
        })
    })

    describe('DELETE non-existent user in the user database', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .delete("/user")
            .query({id: wrong_id})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("no such user!");
            done();
            })
        })
    })

    describe('DELETE existing user in the user database', () => {
        it("This should delete a user", (done) => {
            chai.request(server)
            .delete("/user")
            .query({id: id_to_delete})
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("user is deleted!");
            done();
            })
        })
    })
})