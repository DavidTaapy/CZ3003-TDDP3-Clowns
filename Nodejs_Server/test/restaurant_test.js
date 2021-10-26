import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

describe('Testing Restaurant API (restaurant.controller.js)', () => {

    var validResName = "FineDining";
    var invalidResName = "SushiBar";

    var newRestaurant = {
        "resSource": "Sprites/Single Mode Scene Sprites/FineDiningBg",
        "dishes": [
            "Sprites/Food Sprites/Completed Dishes Sprites/Ramen"
            ],
        "name": "TestBar"
    }

    var validResToDelete = "TestBar";
    var invalidResToDelete = "TesterBar";

    describe('GET Restaurant with valid param', () => {
        it("This should get all dishes for the specified Restaurant", (done) => {
            chai.request(server)
            .get("/restaurant")
            .query({name : validResName})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body[0]).to.have.property('dishes').to.be.an('array');
                expect(res.body[0]).to.have.property('name').eq(validResName);
                expect(res.body[0]).to.have.property('resSource');
            done();
            })
        })
    })

    describe('GET Restaurant with invalid name param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .get("/restaurant")
            .query({name : invalidResName})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("No such restaurant!");
            done();
            })
        })
    })

    describe('GET Restaurant with missing name param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .get("/restaurant")
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing restaurant name param");
            done();
            })
        })
    })

    describe('POST new restaurant dish into the database', () => {
        it("This should add a restaurant dish into the database", (done) => {
            chai.request(server)
            .post("/restaurant")
            .type("JSON")
            .send(newRestaurant)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("restaurant added!");
            done();
            })
        })
    })

    // Test DELETE non-existent restaurant
    describe('DELETE non-existent restaurant in the restaurant database with invalid name param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .delete("/restaurant")
            .query({name : invalidResToDelete})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("No such restaurant!");
            done();
            })
        })
    })

    describe('DELETE restaurant in the restaurant database with missing name param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .delete("/restaurant")
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing restaurant name param!");
            done();
            })
        })
    })

    // Test DELETE existing restaurant
    describe('DELETE existing restaurant in the restaurant database with valid name param', () => {
        it("This should delete a restaurant", (done) => {
            chai.request(server)
            .delete("/restaurant")
            .query({name : validResToDelete})
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("Restaurant is deleted!");
            done();
            })
        })
    })

})