import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

describe('Testing Character API (character.controller.js)', () => {

    var new_char ={
        "characterDescription": "I am a chicken!",
        "spriteSource": "Sprites/",
        "characterName": "Chicken"
    }
    var trial_id = "Wx7ePiSen9NjEL0LtxoK"; 
    
    //Test GET 
    describe('GET character with id', () => {
        it("This should get a character", (done) => {
            chai.request(server)
            .get("/character")
            .query({id: trial_id})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body).to.have.property('characterName');
                expect(res.body).to.have.property('characterDescription');
                expect(res.body).to.have.property('spriteSource');
                expect(res.body).to.have.property('characterID').eq(trial_id);
            done();
            })
        })
    })

    //Test GET with missing id param
    describe('GET character with missing id param', () => {
        it("This should get an error message for missing id param", (done) => {
            chai.request(server)
            .get("/character")
            .query({id: ""})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing id param in request!");
            done();
            })
        })
    })

    //Test POST new character
    describe('POST new character' , () => {
        it("This should add a character", (done) => {
            chai.request(server)
            .post("/character")
            .type("JSON")
            .send(new_char)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("character added!");
            done();
            })
        })
    })

    // Test DELETE character that does not exist
    describe('DELETE character that does not exist', () => {
        it("This should not delete anything and return an error message", (done) => {
            chai.request(server)
            .delete("/character")
            .query({name : "nil"})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("No such character!");
            done();
            })
        })
    })

    // Test DELETE existing item
    describe('DELETE existing character in the database', () => {
        it("This should delete an existing character", (done) => {
            chai.request(server)
            .delete("/character")
            .query({name : "Chicken"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("character is deleted!");
            done();
            })
        })
    })

})