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

describe('User API', () => {
    //Test GET user
    var user_id = "testttt";
    var wrong_id = "wrong";
    var new_user = {
        "id": "1234",
        "userName": "ryannieeeee",
        "primaryLevel": 1
    }

    var change_user = {
        "id": "1234",
        "userName": "ryannieeeee",
        "primaryLevel": 4
    }
    var id_to_delete = "1234";

    describe('GET /user', () => {
        it("This should get the user with corresponding id", (done) => {
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

    describe('GET /user (wrong)', () => {
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
    
    describe('POST /user', () => {
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

    describe('PUT /user', () => {
        it("This should edit a user details", (done) => {
            chai.request(server)
            .put("/user")
            .query({id: id_to_delete})
            .type("JSON")
            .send(change_user)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("user is updated!");
            done();
            })
        })
    })

    describe('DELETE /user', () => {
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