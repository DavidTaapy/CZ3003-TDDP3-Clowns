import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

describe('Item API', () => {

    var validLvl = "1";
    var invalidBiggerLvl = "7";
    var invalidSmallerLvl = "0";

    var new_question = {
        "question": "Testing 1+1",
        "answers": ["0","3","2","1"],
        "correctAnswerIndex" : 1,
        "primaryLevel" : 1,
        "hints" : ["Testing hint"]
    
    }
    var validQnToDelete = "Testing 1+1";
    var invalidQnToDelete = "1+1";
    
    describe('GET questionList with valid param', () => {
        it("This should get all questions for the specified primaryLevel", (done) => {
            chai.request(server)
            .get("/questions")
            .query({lvl : validLvl})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body[0]).to.have.property('hint');
                expect(res.body[0]).to.have.property('question');
                expect(res.body[0]).to.have.property('primaryLevel').eq(parseInt(validLvl));
                expect(res.body[0]).to.have.property('answers');
            done();
            })
        })
    })

    describe('GET questionList with invalid param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .get("/questions")
            .query({lvl : invalidBiggerLvl})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("primaryLevel out of range!");
            done();
            })
        })
    })

    describe('GET questionList with invalid param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .get("/questions")
            .query({lvl : invalidSmallerLvl})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("primaryLevel out of range!");
            done();
            })
        })
    })

    describe('GET questionList with missing param', () => {
        it("This should get all questions for the specified primaryLevel", (done) => {
            chai.request(server)
            .get("/questions")
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing lvl param in request!");
            done();
            })
        })
    })

    describe('POST new question into the database', () => {
        it("This should add a question into the database", (done) => {
            chai.request(server)
            .post("/questions")
            .type("JSON")
            .send(new_question)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("question added!");
            done();
            })
        })
    })

    // Test DELETE non-existent question
    describe('DELETE non-existent question in the questions database with invalid qn param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .delete("/questions")
            .query({qn : invalidQnToDelete})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("No such question!");
            done();
            })
        })
    })

    // Test DELETE non-existent question
    describe('DELETE question in the questions database with missing qn param', () => {
        it("This should return an error message", (done) => {
            chai.request(server)
            .delete("/questions")
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing qn param!");
            done();
            })
        })
    })

    // Test DELETE existing question
    describe('DELETE existing question in the questions database with valid qn param', () => {
        it("This should delete an existing question", (done) => {
            chai.request(server)
            .delete("/questions")
            .query({qn : validQnToDelete})
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("question is deleted!");
            done();
            })
        })
    })


})